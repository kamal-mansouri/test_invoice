/**
 * global variables
 */
var row_counter = 1;

///----------------------show_items-----------------------------------------
/**
 *  request to server for get similar items
 * @param {String} item_type
 * @param {Object} input
 * @param {String} listId
 */
function show_items(item_type, input, listId) {

    var text = input.value;
    var url = "";

    document.getElementById(listId).innerHTML = "";
    input.dataset.isvalid = "false";

    if (text.length < 1) {
        return;
    }

    try {

        var xhttp = new XMLHttpRequest();
        xhttp.onreadystatechange = function () {
            if (this.readyState === 4 && this.status === 200) {
                document.getElementById(listId).innerHTML = this.responseText;
            }
        };

        if (item_type == "customer") {
            url = "/Invoice/GetCustomer";
        } else {
            url = "/Invoice/GetCommodity";
        }

        xhttp.open("POST", url, true);
        xhttp.setRequestHeader("Content-type", "application/x-www-form-urlencoded");
        xhttp.send("text=" + text + "&inputId=" + input.id);
    } catch (e) {
        console.log(e);
    }
}

///-------------------------set_item----------------------------------------------------
/**
 * set item code in textbox data-selectedid and clear dropdown items
 * @param {String} selctedId
 * @param {String} selctedValue
 * @param {String} liId
 */
function set_item(selctedId, selctedValue, liId) {

    var input = $("#" + liId).parent().parent().children("input")[0];
    var ul = $("#" + liId).parent().parent().children("ul")[0];

    input.value = selctedValue;
    input.dataset.isvalid = "true";
    input.dataset.selctedid = selctedId;

    ul.innerHTML = " ";
}

///---------------------------lost_focus---------------------------------------
/**
 * when customer search box onfocus 
 * this function clear list item dropdowned
 * @param {String} inputId
 */
function lost_focus(inputId) {
   
    var input = $("#" + inputId)[0];
    var ul = $("#" + inputId).parent().children("ul")[0];

    if (input.dataset.isvalid == "false") {

        ul.innerHTML = "";
        input.value = "";
    }
}

///-----------------------------add_row---------------------------------------------------
/**
 * add new row to table of invoice detail
 */
function add_row() {

    var tr = document.createElement("tr");
    var row_id = "_new" + (row_counter++);
    tr.id = row_id;

    tr.innerHTML =
        '<td scope="row"></td>'
        + '<td><input id="commodity' + row_id + '" class="form-control" type="text" onkeyup="show_items(&#39;commodity&#39; , this, &#39;commodity_list' + row_id + '&#39;)" onblur="lost_focus(&#39;commodity' + row_id + '&#39;);" data-selctedid="" data-isvalid="" required>'
        + '<ul id="commodity_list' + row_id + '" class="list-group input-list"></ul></td>'
        + '<td><input id="price' + row_id + '" name="price" onkeydown = "return isNumber(event);" onchange="calculate_total();" class="form-control en" type="text" required></td>'
        + '<td><input id="count' + row_id + '" name="count" onkeydown = "return isNumber(event);" onchange="calculate_total();" class="form-control en" type="number" required></td>'
        + '<th id="sum' + row_id + '" class="center">0</th>'
        + '<th><a href="javascript:void(0)" onclick="del_row(&#39;' + row_id + '&#39;)"><span class= "icon icon-del"><i class="fa fa-times-circle"></i></span></a></th>';

    document.getElementById("detail_content").appendChild(tr);

    set_row_order();
}

///-------------------------------del_row-----------------------------------------------
/**
 * remove new row to table of invoice detail
 * @param {string} row_id
 */
function del_row(row_id) {

    if (document.querySelectorAll("tbody > tr").length == 1) {
        return;
    }

    swal({
        
        text: "سطر مورد نظر از فاکتور حذف شود ؟",
        buttons: {
            cancel: "لغو",
            true: {
                text: "تایید",
                value: true,
            }
        },
        dangerMode: true
    })
        .then((willDelete) => {
        if (willDelete) {
            document.getElementById(row_id).remove();
            set_row_order();
        }
    });
}

///----------------------------------set_row_order---------------------------------------
/**
 * شماره ردیف های جدول جزییات فاکتور را درست می کند
 */
function set_row_order() {

    var rows = document.querySelectorAll("tbody > tr");

    for (var i = 0; i < rows.length; i++) {

        rows[i].firstChild.innerHTML = i + 1;
    }
}

///---------------------------------set_invoice_alert---------------------------------------
/**
 * پیام ذخیره فاکتور را نشان میدهد
 * در صورت تایید تابع 
 * set_invoice
 * فراخوانی می شود
 * @param {string} action_type
 * @param {number} invoiceId
 */
function set_invoice_alert(action_type, invoiceId = 0) {
    var isComplate = 'true';

    if (document.querySelectorAll("tbody > tr").length < 1) {
        return;
    }

    document.querySelectorAll('[data-isvalid]').forEach(function (el) {
        if (el.value.length < 1) {
            isComplate = 'false';
            return;
        }
    });

    document.querySelectorAll('input[name="count"] , input[name="price"]').forEach(function (el) {
        if (el.value.length < 1) {
            isComplate = 'false';
            return;
        }
    });

    if (isComplate === 'false') {
        return;
    }

    swal({
        title: "ثبت فاکتور",
        text: "اطلاعات فاکتور ثبت شود؟",
        icon: "warning",
        buttons: {
            cancel: "لغو",
            true: {
                text: "تایید",
                value: true,
            }
        },
        dangerMode: true
    })
        .then((willDelete) => {
            if (willDelete) {
                set_invoice(action_type, invoiceId);
            }
        });
}

///------------------------------------set_invoice-----------------------------------------
/**
 * فرستادن اطلاعات فاکتور به سرور برای افزودن یا ویرایش
 * @param {string} action_type
 * @param {number} invoiceId
 */
function set_invoice(action_type, invoiceId = 0) {

    var invoice = {};
    var Details = [];
    var InvoiceViewModel = {};
    var rows = document.querySelectorAll("tbody > tr");

    invoice.CustomerId = document.getElementById("customer").dataset.selctedid;
    invoice.Discount = document.getElementById("Discount").value;
    invoice.Id = invoiceId;

    for (var i = 0; i < rows.length; i++) {
        var rowId = rows[i].id;
        var InvoiceD = {};

        InvoiceD.CommodityId = document.getElementById("commodity" + rowId).dataset.selctedid;
        InvoiceD.Qty = document.getElementById("count" + rowId).value;
        InvoiceD.Price = document.getElementById("price" + rowId).value;
        InvoiceD.InvoiceId = invoiceId;
        InvoiceD.Id = (rowId.substr(0, 4) === '_new') ? 0 : rowId.substr(1);

        Details.push(InvoiceD);
    }

    InvoiceViewModel.invoice = invoice;
    InvoiceViewModel.invoiceDetails = Details;

    try {

        var xhttp = new XMLHttpRequest();
        xhttp.onreadystatechange = function () {
            if (this.readyState === 4 && this.status === 200) {
                if (this.responseText == "true") {

                    swal("", "عملیات با موفقیت انجام شد", "success");
                    get_section("list");
                } else {
                    swal("", "عملیات انجام نشد", "error");
                }
            }
        };

        var url;
        if (action_type == "add") {

            url = "/Invoice/SetNewInvoice";
        } else if (action_type == "edit") {

            url = "/Invoice/SetEditInvoice";
        }

        xhttp.open("POST", url, true);
        xhttp.setRequestHeader("Content-type", "application/json");
        xhttp.send(JSON.stringify({ invoiceSet: InvoiceViewModel }));
    } catch (e) {
        console.log(e);
        return false;
    }
}

///----------------------------------get_section------------------------------------------
/**
 * هر کدام از بخش ها را از سرور می گیرد و در
 * invoice_container
 * جاگذاری می کند
 * @param {string} section_name
 * @param {number} invoiceId
 * @param {string} title
 */
function get_section(section_name , invoiceId = 0 , title = '' ) {

    try {

        var xhttp = new XMLHttpRequest();
        xhttp.onreadystatechange = function () {
            if (this.readyState === 4 && this.status === 200) {
                document.getElementById("invoice_container").innerHTML = this.responseText;

                if (section_name == "edit" && title != '') {
                    add_header(invoiceId, title);
                }
                else if (section_name == "add") {
                    add_row();
                }
            }
        };

        var url;
        if (section_name == "list") {
            url = "/Invoice/GetList";
        } else if (section_name == "add") {
            url = "/Invoice/AddInvoice";
        } else if (section_name == "edit") {
            url = "/Invoice/EditInvoice/" + invoiceId;
        }

        xhttp.open("POST", url, true);
        xhttp.send();
    } catch (e) {
        console.log(e);
        return false;
    }
}

///----------------------------------add_header-------------------------------------------
/**
 * فاکتوری که برای ویرایش نمایش داده شده 
 * هدری در قسمت 
 * navbar
 * قرار می دهد
 * @param {number} id
 * @param {string} title
 */
function add_header(id, title) {

    if (document.getElementById("li_invoice_" + id)) {

        get_section("edit", id);
        return;
    }

    var li = document.createElement("li");
    li.classList.add("nav-item");
    li.classList.add("li-border");
    li.id = "li_invoice_" + id;

    li.innerHTML =
        '<a class="nav-link" href="javascript:void(0);">'
    + '<span class="icon icon-del" onclick="del_section(' + id + ' , get_section);"><i class="fa fa-times-circle"></i></span>&#160;&#160;'
        + '<span onclick="get_section(&#39;edit&#39;,' + id + ');">فاکتور ' + title + '</span>'
        + '</a>';

    document.getElementById("sidebar_items").appendChild(li);
}

///------------------------------------del_section---------------------------------------
/**
 * 
 * @param {string} id
 * @param {function} callback
 */
function del_section(id , callback) {
    
    document.getElementById("sidebar_items").removeChild(document.getElementById("li_invoice_" + id));
    callback('list');
}

///-----------------------------------calculate_total-----------------------------
/**
 * calculate total of invoice and show it
 */
function calculate_total() {
    var total = 0;

    var rows = document.querySelectorAll("tbody > tr");

    for (var i = 0; i < rows.length; i++) {
        var rowId = rows[i].id;
        var qty = document.getElementById("count" + rowId).value;
        var price = document.getElementById("price" + rowId).value;

        if (isNaN(qty)) {
            qty = 0;
        }

        if (isNaN(price)) {
            price = 0;
        }

        total += qty * price;

        document.getElementById("sum" + rowId).innerText = qty * price;
    }

    document.getElementById("total").innerText = total;
}

///-------------------------------show_new_box----------------------------------------
/**
 * نمایش باکس افزودن مشتری یا کالای جدید
 * فعلا راه اندازی نشده است
 * @param {string} item_type
 * @param {string} value
 */
function show_new_box(item_type, value) {
    swal({
        title: 'افزودن ' + item_type ,
        text: value + '  اضافه شود؟',
        content: "input",
        button: {
            text: "افزودن",
            closeModal: false,
        },
    }).then((value) => {
        swal('این بخش راه اندازی نشده است');
    });
}