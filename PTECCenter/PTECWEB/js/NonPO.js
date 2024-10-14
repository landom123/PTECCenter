function ArabicNumberToText(number) {
    var number = CheckNumber(number);
    if (isNaN(number)) return "ไม่ใช่ตัวเลข";

    const units = ["", "หนึ่ง", "สอง", "สาม", "สี่", "ห้า", "หก", "เจ็ด", "แปด", "เก้า"];
    const places = ["", "สิบ", "ร้อย", "พัน", "หมื่น", "แสน", "ล้าน"];

    function convertChunk(num) {
        let text = "";
        const numStr = num.toString();
        const len = numStr.length;

        for (let i = 0; i < len; i++) {
            const digit = parseInt(numStr[i], 10);
            const place = len - i - 1;

            if (digit === 0) continue;

            if (place === 1 && digit === 1) {
                text += "สิบ";
            } else if (place === 1 && digit === 2) {
                text += "ยี่สิบ";
            } else if (place === 1) {
                text += units[digit] + "สิบ";
            } else if (place === 0 && digit === 1 && len > 1) {
                text += "เอ็ด";
            } else {
                text += units[digit] + places[place];
            }
        }

        return text;
    }

    function convertDecimal(decimal) {
        let decimalText = "";
        const decimalStr = decimal.toString().slice(0, 2); // ใช้ทศนิยมแค่สองตำแหน่ง
        for (let i = 0; i < decimalStr.length; i++) {
            const digit = parseInt(decimalStr[i], 10);
            decimalText += units[digit];
        }
        return decimalText ? decimalText + "สตางค์" : "";
    }

    let [integerPart, decimalPart] = number.toString().split('.');

    // แปลงส่วนจำนวนเต็ม
    let text = "";
    let millionPart = "";

    if (integerPart.length > 6) {
        const millionIndex = integerPart.length - 6;
        millionPart = convertChunk(integerPart.slice(0, millionIndex)) + "ล้าน";
        integerPart = integerPart.slice(millionIndex);
    }

    const remainderPart = convertChunk(integerPart);
    text = millionPart + remainderPart;

    // แปลงส่วนทศนิยม (ถ้ามี)
    let decimalText = decimalPart ? convertDecimal(decimalPart) : "ถ้วน";

    return text + decimalText;
}

function CheckNumber(Number) {
    var decimal = false;
    Number = Number.toString();
    Number = Number.replace(/ |,|บาท|฿/gi, '');
    for (var i = 0; i < Number.length; i++) {
        if (Number[i] == '.') {
            decimal = true;
        }
    }
    if (decimal == false) {
        Number = Number + '.00';
    }
    return Number
}

function calCostTotal(cost, vat, tax) {
    const c_Vat = calVat(cost, vat).toFixed(4);
    const c_Tax = calTax(cost, tax).toFixed(4);
    return cost + parseFloat(c_Vat) - parseFloat(c_Tax);
}


function calTax(cost, tax) {
    return cost * (tax / 100);
}

function calVat(cost, vat) {
    return cost * (vat / 100);
}



/*  ################## Attach #################  */
function chkAttach(elem, userid) {
    //console.log(s)
    //console.log(s.id)
    //console.log(s.checked)

    event.preventDefault();
    var params = "{'attatchid': '" + elem.id + "','chked': '" + elem.checked + "','userid': '" + userid + "'}";
    $.ajax({
        type: "POST",
        url: "/OPS/Non-PO/Advance/ClearAdvance.aspx/changeChecked",
        async: true,
        data: params,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
            //console.log(msg)
            //console.log(msg.d['menuid'])
            var obj = JSON.parse(msg.d);
            console.log(obj)
            console.log(obj[0]['attid'])
            for (let i = 0; i < obj.length; i++) {
                (obj[i]['checked']) ? $('.attatchItems #' + obj[i]['attid']).prop('checked', true) : $('.attatchItems #' + obj[i]['attid']).prop('checked', false);
            }
        },
        error: function () {
            alertWarning('fail')
        }
    });

}

function removeAttach(attachid, userid) {
    Swal.fire({
        title: 'คุุณต้องการจะลบข้อมุลนี้ใช่หรือไม่ ?',
        text: "",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Yes',
        allowOutsideClick: false
    }).then((result) => {
        if (result.isConfirmed) {
            var params = "{'attachid': '" + attachid + "','userid': '" + userid + "'}";
            $.ajax({
                type: "POST",
                url: "/OPS/Non-PO/Payment/Payment2.aspx/deleteAttach",
                async: true,
                data: params,
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (msg) {
                    if (msg.d == 'success') {
                        swal.fire({
                            title: "Deleted!",
                            text: "",
                            icon: "success",
                            allowOutsideClick: false
                        }).then(function () {
                            const attitem = document.getElementById('ATT' + attachid);
                            attitem.parentNode.removeChild(attitem);
                        });
                    } else {
                        alertWarning('fail')
                    }
                },
                error: function () {
                    alertWarning('fail ee')
                }
            });
        }
    })

    return false;
}
/*  ################## END Attach #################  */



/*  ################## comment #################  */

function checkEditcomment(textBox, e, length, Oldtext) {
    var mLen = length;
    var maxLength = parseInt(mLen);
    console.log(Oldtext);
    console.log(textBox.textContent.trim());
    console.log(textBox.textContent.trim().length);
    if (!(e.keyCode == 27)) {
        if (textBox.textContent.trim().length > maxLength - 1) {
            if (window.event)//IE
                e.returnValue = false;
            else//Firefox
                e.preventDefault();
        } else if (event.keyCode == 13) {
            if (event.shiftKey) {
                event.stopPropagation();
            } else {
                updateComment(textBox.id, Oldtext)
            }
        }
    } else {
        cancelEditComment(textBox, Oldtext);
        textBox.textContent = Oldtext;
    }
}

function cancelEditComment(elem, Oldtext) {
    if (elem.textContent.trim() != Oldtext.trim()) {
        Swal.fire({
            title: 'คุุณต้องบันทึกหรือไม่ ?',
            text: "",
            icon: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            confirmButtonText: 'Yes',
            allowOutsideClick: false
        }).then((result) => {
            if (result.isConfirmed) {
                updateComment(elem.id, Oldtext);
            } else {
                elem.textContent = Oldtext;
            }
        })
    } else {
        elem.textContent = Oldtext;
    }
    elem.setAttribute('contenteditable', 'false');

}

function btnEditCommentClick(commentID) {
    const elemenmt = document.getElementById(commentID);
    elemenmt.setAttribute("contenteditable", "true");
    console.log(elemenmt);

    elemenmt.focus();
    console.log('edit');

}

function confirmDelete(commentID, userid) {

    Swal.fire({
        title: 'คุุณต้องการจะลบข้อมุลนี้ใช่หรือไม่ ?',
        text: "",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Yes',
        allowOutsideClick: false
    }).then((result) => {
        if (result.isConfirmed) {
            var params = "{'commentid': '" + commentID + "','userid': '" + userid + "'}";
            $.ajax({
                type: "POST",
                url: "/OPS/Non-PO/Advance/ClearAdvance.aspx/deleteComment",
                async: true,
                data: params,
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (msg) {
                    if (msg.d == 'success') {
                        window.location.href = location.href;
                    } else {
                        alertWarning('Delete fail');
                    }
                },
                error: function () {
                    alertWarning('Delete Comment fail');
                }
            });
        }
    })

    return false;
}
/*  ################## end comment #################  */

//$(".print input:checkbox").on('click', function () {
//    // in the handler, 'this' refers to the box clicked on
//    console.log(this);
//    var $box = $(this);
//    if ($box.is(":checked")) {
//        // the name of the box is retrieved using the .attr() method
//        // as it is assumed and expected to be immutable
//        var group = "input:checkbox";
//        // the checked state of the group/box on the other hand will change
//        // and the current value is retrieved using .prop() method
//        $(group).prop("checked", false);
//        $box.prop("checked", true);
//    } else {
//        $box.prop("checked", false);
//    }
//});

function numberWithCommas(x) {
    return parseFloat(x).toLocaleString(undefined, {
        minimumFractionDigits: 2,
        maximumFractionDigits: 4,
    });
}





/*  ################## autocomplete #################  */


function nonpo_setVendorAutocomplete(taxidno, elemid) {
    //$("#" + elemVendor + " option:contains(" + vendorname + ")")
    //    .attr('selected', 'selected')
    //    .siblings()
    //    .removeAttr("selected");
    //const Acc = document.getElementById(elemVendor);

    //var taxidno = Acc.options[Acc.selectedIndex].getAttribute("data-taxidno");
    //console.log(taxidno);
    console.log(taxidno);

    $("#" + elemid).val(taxidno);

}
function nonpo_autocomplete(inp, arr, arr2, elemtaxid) {

    /*the autocomplete function takes two arguments,
    the text field element and an array of possible autocompleted values:*/
    var currentFocus;
    /*execute a function when someone writes in the text field:*/
    inp.addEventListener("input", function (e) {
        var a, b, i, cnt_res = 0, val = this.value;

        /*close any already open lists of autocompleted values*/
        closeAllLists();
        if (!val) { return false; }
        currentFocus = -1;
        /*create a DIV element that will contain the items (values):*/
        a = document.createElement("DIV");
        a.setAttribute("id", this.id + "autocomplete-list");
        a.setAttribute("class", "autocomplete-items");
        /*append the DIV element as a child of the autocomplete container:*/
        this.parentNode.appendChild(a);
        /*for each item in the array...*/
        for (i = 0; i < arr.length; i++) {
            /*check if the item starts with the same letters as the text field value:*/
            //var cnt_replace = 0
            var index = arr[i].toUpperCase().indexOf(val.toUpperCase());
            ////console.log(cnt_replace);
            if (index > -1) {
                /*create a DIV element for each matching element:*/
                b = document.createElement("DIV");
                /*make the matching letters bold:*/
                //b.innerHTML = "<strong>" + arr[i].substr(index, val.length) + "</strong>";
                //b.innerHTML += arr[i].substr(val.length);
                b.innerHTML = arr[i].substring(0, index) + "<strong>" + arr[i].substring(index, index + val.length) + "</strong>" + arr[i].substring(index + val.length);
                /*insert a input field that will hold the current array item's value:*/
                b.innerHTML += "<input type='hidden' value='" + arr[i] + "'>";
                /*execute a function when someone clicks on the item value (DIV element):*/
                b.addEventListener("click", function (e) {
                    /*insert the value for the autocomplete text field:*/
                    inp.value = this.getElementsByTagName("input")[0].value;
                    /*close the list of autocompleted values,
                    (or any other open lists of autocompleted values:*/
                    nonpo_setVendorAutocomplete(arr2[inp.value], elemtaxid);
                    closeAllLists();
                });
                a.appendChild(b);
                cnt_res += 1;
                //if (cnt_res >= 8) {
                //    break;
                //}
            }
        }
    });
    /*execute a function presses a key on the keyboard:*/
    inp.addEventListener("keydown", function (e) {
        var x = document.getElementById(this.id + "autocomplete-list");
        if (x) x = x.getElementsByTagName("div");
        if (e.keyCode == 40) {
            /*If the arrow DOWN key is pressed,
            increase the currentFocus variable:*/
            currentFocus++;
            /*and and make the current item more visible:*/
            addActive(x);
        } else if (e.keyCode == 38) { //up
            /*If the arrow UP key is pressed,
            decrease the currentFocus variable:*/
            currentFocus--;
            /*and and make the current item more visible:*/
            addActive(x);
        } else if (e.keyCode == 13) {
            /*If the ENTER key is pressed, prevent the form from being submitted,*/
            e.preventDefault();
            if (currentFocus > -1) {
                /*and simulate a click on the "active" item:*/
                if (x) x[currentFocus].click();
            }
        }
        //console.log("keydown");
    });
    function addActive(x) {
        /*a function to classify an item as "active":*/
        if (!x) return false;
        /*start by removing the "active" class on all items:*/
        removeActive(x);
        if (currentFocus >= x.length) currentFocus = 0;
        if (currentFocus < 0) currentFocus = (x.length - 1);
        /*add class "autocomplete-active":*/
        x[currentFocus].classList.add("autocomplete-active");
        //console.log("addActive");

    }
    function removeActive(x) {
        /*a function to remove the "active" class from all autocomplete items:*/
        for (var i = 0; i < x.length; i++) {
            x[i].classList.remove("autocomplete-active");
        }
        //console.log("removeActive");

    }
    function closeAllLists(elmnt) {
        /*close all autocomplete lists in the document,
        except the one passed as an argument:*/
        var x = document.getElementsByClassName("autocomplete-items");
        for (var i = 0; i < x.length; i++) {
            if (elmnt != x[i] && elmnt != inp) {
                x[i].parentNode.removeChild(x[i]);
            }
        }
        //console.log("closeAllLists");

    }
    /*execute a function when someone clicks in the document:*/
    document.addEventListener("click", function (e) {
        closeAllLists(e.target);
        //console.log("click");
    });
}
function nonpo_autocomplete_invoice(inp, arrlist, arr2, elemindateid, elemvenid,elemtaxid) {

    /*the autocomplete function takes two arguments,
    the text field element and an array of possible autocompleted values:*/
    var currentFocus;
    /*execute a function when someone writes in the text field:*/
    inp.addEventListener("input", function (e) {
        var a, b, i, cnt_res = 0, val = this.value;

        /*close any already open lists of autocompleted values*/
        closeAllLists();
        if (!val) { return false; }
        currentFocus = -1;
        /*create a DIV element that will contain the items (values):*/
        a = document.createElement("DIV");
        a.setAttribute("id", this.id + "autocomplete-list");
        a.setAttribute("class", "autocomplete-items");
        /*append the DIV element as a child of the autocomplete container:*/
        this.parentNode.appendChild(a);
        /*for each item in the array...*/
        for (i = 0; i < arr.length; i++) {
            /*check if the item starts with the same letters as the text field value:*/
            //var cnt_replace = 0
            var index = arr[i].toUpperCase().indexOf(val.toUpperCase());
            ////console.log(cnt_replace);
            if (index > -1) {
                /*create a DIV element for each matching element:*/
                b = document.createElement("DIV");
                /*make the matching letters bold:*/
                //b.innerHTML = "<strong>" + arr[i].substr(index, val.length) + "</strong>";
                //b.innerHTML += arr[i].substr(val.length);
                b.innerHTML = arr[i].substring(0, index) + "<strong>" + arr[i].substring(index, index + val.length) + "</strong>" + arr[i].substring(index + val.length);
                /*insert a input field that will hold the current array item's value:*/
                b.innerHTML += "<input type='hidden' value='" + arr[i] + "'>";
                /*execute a function when someone clicks on the item value (DIV element):*/
                b.addEventListener("click", function (e) {
                    /*insert the value for the autocomplete text field:*/
                    inp.value = this.getElementsByTagName("input")[0].value;
                    /*close the list of autocompleted values,
                    (or any other open lists of autocompleted values:*/
                    nonpo_setVendorAutocomplete(arr2[inp.value], elemtaxid);
                    closeAllLists();
                });
                a.appendChild(b);
                cnt_res += 1;
                //if (cnt_res >= 8) {
                //    break;
                //}
            }
        }
    });
    /*execute a function presses a key on the keyboard:*/
    inp.addEventListener("keydown", function (e) {
        var x = document.getElementById(this.id + "autocomplete-list");
        if (x) x = x.getElementsByTagName("div");
        if (e.keyCode == 40) {
            /*If the arrow DOWN key is pressed,
            increase the currentFocus variable:*/
            currentFocus++;
            /*and and make the current item more visible:*/
            addActive(x);
        } else if (e.keyCode == 38) { //up
            /*If the arrow UP key is pressed,
            decrease the currentFocus variable:*/
            currentFocus--;
            /*and and make the current item more visible:*/
            addActive(x);
        } else if (e.keyCode == 13) {
            /*If the ENTER key is pressed, prevent the form from being submitted,*/
            e.preventDefault();
            if (currentFocus > -1) {
                /*and simulate a click on the "active" item:*/
                if (x) x[currentFocus].click();
            }
        }
        //console.log("keydown");
    });
    function addActive(x) {
        /*a function to classify an item as "active":*/
        if (!x) return false;
        /*start by removing the "active" class on all items:*/
        removeActive(x);
        if (currentFocus >= x.length) currentFocus = 0;
        if (currentFocus < 0) currentFocus = (x.length - 1);
        /*add class "autocomplete-active":*/
        x[currentFocus].classList.add("autocomplete-active");
        //console.log("addActive");

    }
    function removeActive(x) {
        /*a function to remove the "active" class from all autocomplete items:*/
        for (var i = 0; i < x.length; i++) {
            x[i].classList.remove("autocomplete-active");
        }
        //console.log("removeActive");

    }
    function closeAllLists(elmnt) {
        /*close all autocomplete lists in the document,
        except the one passed as an argument:*/
        var x = document.getElementsByClassName("autocomplete-items");
        for (var i = 0; i < x.length; i++) {
            if (elmnt != x[i] && elmnt != inp) {
                x[i].parentNode.removeChild(x[i]);
            }
        }
        //console.log("closeAllLists");

    }
    /*execute a function when someone clicks in the document:*/
    document.addEventListener("click", function (e) {
        closeAllLists(e.target);
        //console.log("click");
    });
}
/*  ################## END autocomplete #################  */
function removeElem(nameele) {
    let elements = document.getElementsByName(nameele);
    //console.log(elements);
    //console.log("asdasd");
    for (let i = 0; i < elements.length; i++) {
        elements[i].remove();
    }
}

function showBtnSpiner(elemHide) {
    $("#loader").show();
    $(elemHide).hide();
    console.log(elemHide)
    elemHide.parentNode.insertAdjacentHTML(
        'beforeend',
       `<button class="btn btn-secondary" type="button" disabled>
            <span class="spinner-border spinner-border-sm" role="status" aria-hidden="true"></span>
            Loading...
        </button>`
    );
}