function pay_momo() {
    document.getElementById("momo").style.display = "block";
    run_count();
}


var m = 00; // Phút
var s = 60;
var btn = false;
function run_count() {
    if (s < 10) {
        document.getElementById("btn_pay").innerHTML = `00:0${s}`;
    } else {
        document.getElementById("btn_pay").innerHTML = `00:${s}`;
    }

    if (s == 0) {
        clearInterval(timeout);
        document.getElementById("momo").style.display = "none";
        document.getElementById("btn_pay").innerHTML = "Giao dịch thất bại";
        alert("Giao dịch thất bại");
        return;
    }
    if (btn == true) {
        clearInterval(timeout);
        document.getElementById("momo").style.display = "none";
        alert("Đã xác nhận, Giao dịch đang được xử lý");
        return;
    }
    timeout = setTimeout(function () {
        s--;
        run_count();
    }, 1000);
}

function thanhtoan() {
    btn = true;
}