function ArabicNumberToText(Number) {
    var Number = CheckNumber(Number);
    var NumberArray = new Array("ศูนย์", "หนึ่ง", "สอง", "สาม", "สี่", "ห้า", "หก", "เจ็ด", "แปด", "เก้า", "สิบ");
    var DigitArray = new Array("", "สิบ", "ร้อย", "พัน", "หมื่น", "แสน", "ล้าน");
    var BahtText = "";
    if (isNaN(Number)) {
        return "ข้อมูลนำเข้าไม่ถูกต้อง";
    } else {
        if ((Number - 0) > 9999999.9999) {
            return "ข้อมูลนำเข้าเกินขอบเขตที่ตั้งไว้";
        } else {
            Number = Number.split(".");
            if (Number[1].length > 0) {
                Number[1] = Number[1].substring(0, 2);
            }
            var NumberLen = Number[0].length - 0;
            for (var i = 0; i < NumberLen; i++) {
                var tmp = Number[0].substring(i, i + 1) - 0;
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

function confirmDelete(commentID) {

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
            var params = "{'commentID': '" + commentID + "'}";
            $.ajax({
                type: "POST",
                url: "../OPS/Master.aspx/asaaa",
                async: true,
                data: params,
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (msg) {
                    window.location.href = location.href;
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

$(".print input:checkbox").on('click', function () {
    // in the handler, 'this' refers to the box clicked on
    console.log(this);
    var $box = $(this);
    if ($box.is(":checked")) {
        // the name of the box is retrieved using the .attr() method
        // as it is assumed and expected to be immutable
        var group = "input:checkbox";
        // the checked state of the group/box on the other hand will change
        // and the current value is retrieved using .prop() method
        $(group).prop("checked", false);
        $box.prop("checked", true);
    } else {
        $box.prop("checked", false);
    }
});