/*
 * 	GARAGEDOOR MENU 
 * 	A Javascript jQuery script by Gaya Kessler
 * 	Version 1.0
 * 	Date: 08-04-09
 * 
 */
var _color = "#FFFFFF";
var GarageDoor = {

    scrollY: 0,
    scrollX: 0,

    setBindings: function (id) {
        $("#" + id + " .mouse").each(function (i) {
            $(this).bind("mouseenter", function (e) {
               
                GarageDoor.hideDoor(this);
            });
            $(this).bind("mouseleave", function (e) {
                GarageDoor.showDoor(this);
            });

            $(this).bind("click", function (e) {
                //window.location = this.parentNode.title;
                //data-skin="winter" data-caption="TPM - Login" data-width="300px" data-height="200px" data-effect="slideBottom" 

                var skinName = 'dark';
                var css3Effects = '';
                var effect = 'slideBottom';
                var href = this.parentNode.dir;// 'about:blank'; //|| theme_url + "assets/images/homeSlide89.png";
                var width = $(window).width() / 100 * 90;
                var height = $(window).height() / 100 * 90;

                $("body").speedoPopup(
			        {
			            href: href,
			            height: height,
			            width: width,
			            theme: skinName,
			            unload: true,
			            draggable: true,
			            effectIn: effect,
			            effectOut: effect,
			            css3Effects: css3Effects
			        });
                AddSubPageTitle(this.parentNode.title, width + 'px');
                return false;

                //
            });
        });
    },

    hideDoor: function (obj) {
        var xs = GarageDoor.scrollX;
        var ys = GarageDoor.scrollY;
        
        $(obj).parent().find(".overlay").each(function (i) {
            $(this).stop().animate({
                marginTop: ys + "px"
            }, 200);
        });
    },

    showDoor: function (obj) {
        $(obj).parent().find(".overlay").each(function (i) {
            $(this).stop().animate({
                marginTop: "0px"
            }, 500);
        });
    }
}