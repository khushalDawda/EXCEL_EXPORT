
$(document).ready(function () {
    GetAccount();
});

function GetAccount() {
    $.ajax({
        url: '/Account/GetAccountsFor',
        type: 'Get',
        dataType: 'json',
        success: onSucess
    })
}

function onSucess(response) {
    $('#AccountDatatable1').DataTable({
        processing: true,
        lengthChange: true,
        ordering: true,
        pagingType: "full_numbers",
        lengthMenu: [[5, 10, 25, -1], [5, 10, 25, "All"]],
        data: response,
        columns: [
            {
                data: 'Id',
                render: function (data, type, row, meta) {
                    return row.id
                }
            },
            {
                data: 'AccountNo',
                render: function (data, type, row, meta) {
                    return row.accountNo
                }
            },
            {
                data: 'GL_NAME',
                render: function (data, type, row, meta) {
                    return row.gL_NAME
                }
            },
            {
                data: 'Balance',
                render: function (data, type, row, meta) {
                    return row.balance
                }
            },
        ],
        footerCallback: function (row, data, start, end, display) {
            var api = this.api();
            $(api.column(0).footer()).html('Total');
            var totalQTY = 0;
            data.forEach(element => {
                console.log('element    ===> ', element.balance)
                totalQTY += parseFloat(element.balance);
            });

            $(api.column(3).footer()).html(totalQTY);
            console.log('total qty', totalQTY)
        }

    });

}
