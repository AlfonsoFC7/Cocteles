
function BuscarInput() {
    debugger;
    var datTxt = Document.getElementById("textSearch").value;
    console.log(datTxt);
    var url = @Url.Action("Index", "Home");
    var urlf = url + datTxt;
    $.get(urlf).done(function (data) {
        console.log(data);
    });
}


