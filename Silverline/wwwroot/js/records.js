var excelJson = "";

function addData() {
    var regex = /^([a-zA-Z0-9\s_\\.\-:])+(.xlsx|.xls)$/;
    var excelFile = $('#excelFile').val();

    if (regex.test(excelFile.toLowerCase())) {
        var xlsxFlag = false;

        if (excelFile.toLowerCase().indexOf(".xlsx") > 0) {
            xlsxFlag = true;
        }

        if (typeof (FileReader) != "undefined") {
            var reader = new FileReader();
            reader.onload = function (e) {
                var data = e.target.result;
                var workbook;
                if (xlsxFlag) {
                    workbook = XLSX.read(data, {
                        type: 'binary'
                    });
                } else {
                    workbook = XLS.read(data, {
                        type: 'binary'
                    });
                }

                var sheetNameList = workbook.SheetNames;

                if (xlsxFlag) {
                    excelJson = XLSX.utils.sheet_to_json(workbook.Sheets[sheetNameList[0]]);
                    if (excelJson.length == 0) {
                        $.dialog({
                            title: "Warning",
                            content: "Excel is empty",
                            buttons: [{
                                type: "confirm",
                                text: "Ok"
                            }]
                        }).open();
                        return false;
                    }
                } else {
                    excelJson = XLS.utils.sheet_to_row_object_array(workbook.Sheets[y]);
                }

                populateData(excelJson);

            };
            if (xlsxFlag) {
                reader.readAsArrayBuffer($('#excelFile').prop('files')[0])
            } else {
                reader.readAsBinaryString($('#excelFile').prop('files')[0])
            }
        } else {
            alert("Sorry! Your browser does not support HTML5.");
        }
    } else {
        alert("Please upload a valid Excel file.");
    }
}

function populateData(jsonData) {
    var html = '';
    for (var i = 0; i < jsonData.length; i++) {
        html += '<tr>';
        html += '<td>' + jsonData[i]["Specialty"] + '</td>';
        html += '<td>' + jsonData[i]["Date of Appointment"] + '</td>';
        html += '<td>' + jsonData[i]["Doctor Name"] + '</td>';
        html += '<td>' + jsonData[i]["Diagnosis Title"] + '</td>';
        html += '<td>' + jsonData[i]["Description"] + '</td>';
        html += '<td>' + jsonData[i]["Prescriptions"] + '</td>';
        html += '<td>' + jsonData[i]["Laboratory Diagnosis"] + '</td>';
        html += '</tr>';
    }
    $('#dataOutput').html(html);
    var pushButton = $("#push")[0];
    pushButton.style.display = "block";
}

function importEmployees() {
    var data = excelJson;
    var records = [];
    for (var i = 0; i < data.length; i++) {
        records.push(
            {
                "Specialty": data[i]["Specialty"],
                "DateOfAppointment": data[i]["Date of Appointment"],
                "DoctorName": data[i]["Doctor Name"],
                "Title": data[i]["Diagnosis Title"],
                "Description": data[i]["Description"],
                "Medicines": data[i]["Prescriptions"],
                "LaboratoryTests": data[i]["Laboratory Diagnosis"],
            }
        );
    }
    console.log(records);
    $.ajax({
        url: "MedicalRecord/AddRecords",
        method: "POST",
        dataType: "text",
        data: { medicalRecords: records },
        success: function (result) {
            alert("Values successfully added.");
        },
        error: function (errorMessage) {
            alert(errorMessage.responseText);
        }
    });
}