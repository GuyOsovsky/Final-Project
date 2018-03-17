function NameValidation(oSrc, args) {
    var letters = /^[A-Za-z\sא-ת]+$/g;
    args.IsValid = args.Value.match(letters) ? true : false;
}

function NumberValidation(oSrc, args) {
    var decimals = /^[\d]+$/;
    args.IsValid = args.Value.match(decimals) ? true : false;
}