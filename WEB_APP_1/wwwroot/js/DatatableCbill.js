
$(document).ready(function () {
    GetCbill();
});

function GetCbill() {
    $.ajax({
        url: '/Cbill/GetCbillsFor',
        type: 'Get',
        dataType: 'json',
        success: onSucess
    })
}

function onSucess(response) {
    $('#CbillDatatable1').DataTable({
        processing: true,
        lengthChange: true,
        ordering: true,
        pagingType: "full_numbers",
        lengthMenu: [[5, 10, 25, -1], [5, 10, 25, "All"]],
        data: response,
        columns: [
            {
                data: 'Account No',
                render: function (data, type, row, meta) {
                    return row.cbillId
                }
            },
            {
                data: 'Name',
                render: function (data, type, row, meta) {
                    return row.accountName
                }
            },
            {
                data: 'PAN NO',
                render: function (data, type, row, meta) {
                    return row.pancardNo
                }
            },
            {
                data: 'AadharCardNo',
                render: function (data, type, row, meta) {
                    return row.aadharCardNo
                }
            },
            {
                data: 'ElectionCardNo',
                render: function (data, type, row, meta) {
                    return row.electionCardNo
                }
            },
            {
                data: 'SocietyName',
                render: function (data, type, row, meta) {
                    return row.societyName
                }
            },
            {
                data: 'BranchName',
                render: function (data, type, row, meta) {
                    return row.branchName
                }
            },
            {
                data: 'MobileNo',
                render: function (data, type, row, meta) {
                    return row.mobileNo
                }
            },
            {
                data: 'GLNAME',
                render: function (data, type, row, meta) {
                    return row.glname
                }
            },
            {
                data: 'Amount',
                render: function (data, type, row, meta) {
                    return row.amount
                }
            },
        ],
        //footerCallback: function (row, data, start, end, display) {
        //    var api = this.api();
        //    $(api.column(0).footer()).html('Total');
        //    var totalQTY = 0;
        //    data.forEach(element => {
        //        console.log('element    ===> ', element.balance)
        //        totalQTY += parseFloat(element.balance);
        //    });

        //    $(api.column(3).footer()).html('$' + totalQTY);
        //    console.log('total qty', totalQTY)
        //}

    });

}
