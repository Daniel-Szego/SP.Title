
// initialising the color picker UI element for textbox

$(document).ready(function () {
    $('.colorpickerField1 , .colorpickerField2 , .colorpickerField3 , .colorpickerField4').ColorPicker({
        onSubmit: function (hsb, hex, rgb, el) {
            $(el).val(hex);
            $(el).ColorPickerHide();
        },
        onBeforeShow: function () {
            $(this).ColorPickerSetColor(this.value);
        }
    }).bind('keyup', function () {
    $(this).ColorPickerSetColor(this.value);
    });
});

// initializing the colorpicker UI for widget

//$(document).ready(function () {
//    $('#colorSelector').ColorPicker({
//        color: '#0000ff',
//        onShow: function (colpkr) {
//            $(colpkr).fadeIn(500);
//            return false;
//        },
//        onHide: function (colpkr) {
//            $(colpkr).fadeOut(500);
//            return false;
//        },
//        onChange: function (hsb, hex, rgb) {
//            $('#colorSelector div').css('backgroundColor', '#' + hex);
//        }
//    });
//});

