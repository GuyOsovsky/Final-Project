function StartDropDown(Obj, Length){

}

function movedown(Obj, Ypos){
    Obj.style.top = parseInt(Obj.style.top) + 1 + 'px';
    if (parseInt(Obj.style.top) < Ypos) {
        setTimeout(movedown(Obj, Ypos), 20);
    }
}
