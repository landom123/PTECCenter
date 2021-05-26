function show(target){
    document.getElementById(target).style.display = 'block';
    //document.getElementById("ShowID").style.display = 'none';
}

function hide(target){
    document.getElementById(target).style.display = 'none';
    //document.getElementById("HideID").style.display = 'block';
}

function chkNew() {
    var confirmation = window.confirm("ต้องการเพิมข้อมูล? คลิก OK");
    //var confirmation = window.confirm("Are you sure?");
    //document.getElementById("HiddenFieldNew")["value"] = confirmation;
    if (confirmation == true) { window.location.href = "../OPS/Assets.aspx?status=new"; }
    else { return false;}
}

function chkSave() {
    var confirmation = window.confirm("Are you sure?");
    return true;
}

function chkCancel(target) {
    var confirmation = window.confirm("ต้องการยกเลิกรายการ แน่ใจหรือไม่ ?");
    if (confirmation == true)
    { window.location.href = target; }
    return true;
}

function chkCloseJob(target) {
    var confirmation = window.confirm("ต้องการปิดงาน แน่ใจหรือไม่ ?");
    return confirmation;
}

function chkConfirm(target) {
    var confirmation = window.confirm("ต้องการยืนยันรายการ แน่ใจหรือไม่ ?");
    if (confirmation == true) { window.location.href = target; }
    return true;
}
function chkDel(target) {
    var confirmation = window.confirm("ต้องการลบข้อมูล แน่ใจหรือไม่ ?");
    if (confirmation == true)
    { window.location.href = target;}
    return true;
}
function find(target,title) {
    var asset = prompt(title, "xxx");
    if (asset != null) {
        window.location.href = target + asset;
    }
}

function chkButtonCancel(target, status) {
    if (status == "cancel"||status=="new") {
        document.getElementById(target).disabled = true;
    }
}
