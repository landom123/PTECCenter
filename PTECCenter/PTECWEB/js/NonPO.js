function ArabicNumberToText(Number) {
    var Number = CheckNumber(Number);
    var NumberArray = new Array("ศูนย์", "หนึ่ง", "สอง", "สาม", "สี่", "ห้า", "หก", "เจ็ด", "แปด", "เก้า", "สิบ");
    var DigitArray = new Array("", "สิบ", "ร้อย", "พัน", "หมื่น", "แสน", "ล้าน","สิบ", "ร้อย", "พัน", "หมื่น", "แสน");
    var BahtText = "";
    if (isNaN(Number)) {
        return "ข้อมูลนำเข้าไม่ถูกต้อง";
    } else {
        if ((Number - 0) > 999999999.9999) {
            return "ข้อมูลนำเข้าเกินขอบเขตที่ตั้งไว้";
        } else {
            Number = Number.split(".");
            //console.log(Number);
            if (Number[1].length > 0) {
                Number[1] = Number[1].substring(0, 2);
            }
            if (Number[0][0] == '-') {
                BahtText += 'ลบ'
                Number[0] = Number[0].replace("-", "");
            }
            //console.log(Number);
            //console.log(Number[0].indexOf('-'));

            var NumberLen = Number[0].length - 0;
            //console.log(NumberLen);

            for (var i = 0; i < NumberLen; i++) {
                var tmp = Number[0].substring(i, i + 1) - 0;
                //console.log(tmp);
                if (tmp != 0) {
                    if ((i == (NumberLen - 1)) && (tmp == 1)) {
                        BahtText += "เอ็ด";
                    } else
                        if ((i == (NumberLen - 2)) && (tmp == 2)) {
                            BahtText += "ยี่";
                        } else
                            if ((i == (NumberLen - 2)) && (tmp == 1)) {
                                BahtText += "";
                            } else {
                                BahtText += NumberArray[tmp];
                            }
                    BahtText += DigitArray[NumberLen - i - 1];
                }
            }
            BahtText += "บาท";
            if ((Number[1] == "0") || (Number[1] == "00")) {
                BahtText += "ถ้วน";
            } else {
                DecimalLen = Number[1].length - 0;
                for (var i = 0; i < DecimalLen; i++) {
                    var tmp = Number[1].substring(i, i + 1) - 0;
                    if (tmp != 0) {
                        if ((i == (DecimalLen - 1)) && (tmp == 1)) {
                            BahtText += "เอ็ด";
                        } else
                            if ((i == (DecimalLen - 2)) && (tmp == 2)) {
                                BahtText += "ยี่";
                            } else
                                if ((i == (DecimalLen - 2)) && (tmp == 1)) {
                                    BahtText += "";
                                } else {
                                    BahtText += NumberArray[tmp];
                                }
                        BahtText += DigitArray[DecimalLen - i - 1];
                    }
                }
                BahtText += "สตางค์";
            }
            return BahtText;
        }
    }
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
    return (cost + (cost * (vat / 100))) - (cost * (tax / 100));
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
    return x.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");
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
