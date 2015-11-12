function toMale() {
    $('#Female').removeClass('btn-danger');
    $('#Female').addClass('btn-default');
    $('#Male').removeClass('btn-default');
    $('#Male').addClass('btn-primary');
    $('#Gender').val('0');
}

function toFemale() {
    $('#Female').removeClass('btn-default');
    $('#Female').addClass('btn-danger');
    $('#Male').removeClass('btn-primary');
    $('#Male').addClass('btn-default');
    $('#Gender').val('1');
}