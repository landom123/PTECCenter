function getStarRating(id) {
    //console.log(id);
    //console.log(`id is ${id}`);
    const elid = document.getElementById(id);
    //console.log(`elid is ${elid}`);
    if (elid != null) {
        return [...elid.getElementsByClassName("rating__star")];
    }
    else {
        return false;
    }
}

function getRate(stars) {
    const starClassUnactive = "rating__star far fa-circle";
    //console.log("#########################" )
    //console.log(typeof stars)
    console.log(stars)
    let  r=5;
    stars.map((star) => {
        //console.log(star.className.indexOf(starClassUnactive))

        if (star.className.indexOf(starClassUnactive) !== -1) {
            r--;
        } 
    });
    console.log(`r is ${r}`)

    return r;
}


function executeRating(stars) {
    const starClassActive = [
        "rating__star fas fa-angry"
        , "rating__star fas fa-frown"
        , "rating__star fas fa-meh"
        , "rating__star fas fa-smile"
        , "rating__star fas fa-smile-beam"
    ];
    const starClassUnactive = "rating__star far fa-circle";
    const starsLength = stars.length;
    let i, r;
    let totalRateid;
    stars.map((star) => {
        star.onclick = () => {
            i = stars.indexOf(star);
            console.log(`star.parentNode.className is ${star.parentNode.className}`);

            if (star.className.indexOf(starClassUnactive) !== -1) {

                /*printRatingResult(result, i + 1);*/
                for (i; i >= 0; --i) stars[i].className = starClassActive[i];
            } else {

                /*printRatingResult(result, i);*/
                for (i; i < starsLength; ++i) stars[i].className = starClassUnactive;
            }
            if (star.parentNode.className.indexOf("_Service") > -1) {
                calProgressTotal("_Service")
            } else if ((star.parentNode.className.indexOf("_Operator") > -1)) {
                calProgressTotal("_Operator")
            }

        };
    });
}

function executeRatingAvg(stars, avg = 0) {
    const starClassActive = [
        "rating__star fas fa-angry"
        , "rating__star fas fa-frown"
        , "rating__star fas fa-meh"
        , "rating__star fas fa-smile"
        , "rating__star fas fa-smile-beam"
    ];
    const starClassUnactive = "rating__star far fa-circle";
    const starsLength = stars.length;
    for (i = 0; i < starsLength; i++) (i < avg) ? stars[i].className = starClassActive[i] : stars[i].className = starClassUnactive
    //disableOnClickRating(stars);
}

function disableOnClickRating(stars) {
    stars.map((star) => {
        star.onclick = null;
    });
}

function calProgressTotal(className) {
    const starClassUnactive = "rating__star far fa-circle";

    const elrate = [...document.getElementsByClassName("rating"+className)];
    //elrate.shift();
    let elid;
    let cnt;
    const elrateLength = elrate.length;
    //console.log(`className is ${className}`);
    let res = {};
    elrate.map((c) => {
        cnt = 0;
        elid = getStarRating(c.id)
        elid.map((ratingStar) => {

            if (ratingStar.className.indexOf(starClassUnactive) == -1) cnt++;

        })
        res[cnt] = parseInt(res[cnt] ?? 0) + 1;
    })
    const response = Object.keys(res).filter((x) => x > 0);
    let avg = 0;
    let totalselected = 0;
    resetProgressTotal(className)
    response.map((index) => {
        setProgressTotal(index + "star" + className, res[index], elrateLength);

        avg = parseInt(avg) + parseInt(index) * res[index];
        totalselected = totalselected + res[index];
    })
    avg = avg / totalselected;
    setProgressAvg(avg, className)
}

function setProgressAvg(value = 0, className) {
    document.getElementById("txtAvg" + className).innerHTML = (value || 0).toFixed(2);

    let ratingStars = getStarRating("p__all" + className);
    if (ratingStars) executeRatingAvg(ratingStars, Math.floor(value || 0));

}

function setProgressTotal(id, value = 0, max = 0) {
    //console.log(`id is ${id}`);

    const pro = document.getElementById(id);
    pro.value = value;
    pro.max = max;
}

function resetProgressTotal(elid) {
    for (i = 1; i <= 5; ++i) setProgressTotal(i + "star"+elid, 0, 0);
}

function printRatingResult(result, num = 0) {
    result.textContent = `${num}/5`;
}

