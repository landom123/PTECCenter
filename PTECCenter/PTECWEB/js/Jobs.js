function alertSuccess() {
    Swal.fire(
        'สำเร็จ',
        '',
        'success'
    )
}

function alertWarning(massage) {
    Swal.fire(
        massage,
        '',
        'warning'
    )
}

function changeColorBadges() {
    const arrs_app = document?.querySelectorAll('.badgestatus_app');
    //console.log(arrs_app)
    for (let i = 0; i < arrs_app.length; i++) {
        let st_name = arrs_app[i].textContent;
        console.log(st_name)
        arrs_app[i].style.color = "gray";
        switch (st_name) {
            case "แจ้งงาน":
                arrs_app[i].style.backgroundColor = "LightBlue";
                break;
            case "ยกเลิก":
                arrs_app[i].style.backgroundColor = "LightGray";
                break;
            case "ยืนยันแจ้งงาน":
                arrs_app[i].style.backgroundColor = "LightYellow";
                break;
            case "ปิดงาน":
                arrs_app[i].style.backgroundColor = "Gray";
                arrs_app[i].style.color = "black";
                break;
            case "ล็อกค่าใช้จ่ายเรียบร้อยแล้ว":
                arrs_app[i].style.backgroundColor = "Gray";
                arrs_app[i].style.color = "black";
                break;
            case "ผู้แจ้ง Confirm":
                arrs_app[i].style.backgroundColor = "GreenYellow";
                break;
            default:
                arrs_app[i].style.backgroundColor = "Pink";
        }
    }
}


document.querySelector('#gtContent1')?.addEventListener('click', function (e) {
    e.preventDefault();
    $('#collapseOne').collapse('show');
    document.querySelector('#headingOne').scrollIntoView({
        behavior: 'smooth'
    });
});
document.querySelector('#gtContent2')?.addEventListener('click', function (e) {
    e.preventDefault();
    $('#collapseTwo').collapse('show');
    document.querySelector('#headingTwo').scrollIntoView({
        behavior: 'smooth'
    });

});
document.querySelector('#gtContent3')?.addEventListener('click', function (e) {
    e.preventDefault();
    $('#collapseThree').collapse('show');
    document.querySelector('#headingThree').scrollIntoView({
        behavior: 'smooth'
    });
});
document.querySelector('#gtContent4')?.addEventListener('click', function (e) {
    e.preventDefault();
    $('#collapseFour').collapse('show');
    document.querySelector('#headingFour').scrollIntoView({
        behavior: 'smooth'
    });

});
document.querySelector('#gtContent5')?.addEventListener('click', function (e) {
    e.preventDefault();
    document.querySelector('#card_attatch').scrollIntoView({
        behavior: 'smooth'
    });
});
document.querySelector('#gtContent6')?.addEventListener('click', function (e) {
    e.preventDefault();
    document.querySelector('#card_comment').scrollIntoView({
        behavior: 'smooth'
    });
});

$(document).ready(function () {

    changeColorBadges();

    $(window).scroll(function () {
        if ($(this).scrollTop() > 50) {
            $('#back-to-top').fadeIn();
        } else {
            $('#back-to-top').fadeOut();
        }
    });
    // scroll body to 0px on click
    $('#back-to-top').click(function () {
        $('body,html').animate({
            scrollTop: 0
        }, 400);
        return false;
    });
});


