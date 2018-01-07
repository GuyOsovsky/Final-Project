function idValidation(obj) {
    var id = document.getElementById("IDIN").value;
    var checkDigit = id % 10;
    id = parseInt(id /10);
    var sum = idSum(id);
    var isValid = checkId(sum, checkDigit);
    alert(isValid);
}
function idSum(id) {
    if (id > 99999999)
        return -1;
    var mulArr = [2, 1, 2, 1, 2, 1, 2, 1];
    var sum = 0;
    for (var i = 0 ; i < 8; i++) {
        var temp = (id % 10) * mulArr[i]
        if (temp > 9) {
            temp = parseInt(temp % 10) + parseInt((temp / 10) % 10);
        }
        sum += temp;
        id = parseInt(id / 10);
    }
    return sum;
}
function checkId(sum, digit) {
    if ((sum + digit) % 10 == 0)
        return true;
    if ((sum - digit) % 10 == 0)
        return true;
    return false;
}