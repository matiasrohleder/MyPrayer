function ShowLoadingModal() {
    swal({
        allowEscapeKey: false,
        allowOutsideClick: false,
        html: '<img src="/images/loading-gif.gif" style="width:100px">',
        showConfirmButton: false,
        width: '200px'
    });
}

$('option').mousedown(function (e) {
    e.preventDefault();
    $(this).prop('selected', !$(this).prop('selected'));
    return false;
});