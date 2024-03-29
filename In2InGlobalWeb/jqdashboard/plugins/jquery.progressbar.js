﻿/**
 * jQuery EasyUI 1.2.5
 * 
 * Licensed under the GPL terms
 * To use it on other terms please contact us
 *
  
 * 
 */
(function($){
function _1(_2){
$(_2).addClass("progressbar");
$(_2).html("<div class=\"progressbar-text\"></div><div class=\"progressbar-value\">&nbsp;</div>");
return $(_2);
};
function _3(_4,_5){
var _6=$.data(_4,"progressbar").options;
var _7=$.data(_4,"progressbar").bar;
if(_5){
_6.width=_5;
}
if($.boxModel==true){
_7.width(_6.width-(_7.outerWidth()-_7.width()));
}else{
_7.width(_6.width);
}
_7.find("div.progressbar-text").width(_7.width());
};
$.fn.progressbar=function(_8,_9){
if(typeof _8=="string"){
var _a=$.fn.progressbar.methods[_8];
if(_a){
return _a(this,_9);
}
}
_8=_8||{};
return this.each(function(){
var _b=$.data(this,"progressbar");
if(_b){
$.extend(_b.options,_8);
}else{
_b=$.data(this,"progressbar",{options:$.extend({},$.fn.progressbar.defaults,$.fn.progressbar.parseOptions(this),_8),bar:_1(this)});
}
$(this).progressbar("setValue",_b.options.value);
_3(this);
});
};
$.fn.progressbar.methods={options:function(jq){
return $.data(jq[0],"progressbar").options;
},resize:function(jq,_c){
return jq.each(function(){
_3(this,_c);
});
},getValue:function(jq){
return $.data(jq[0],"progressbar").options.value;
},setValue:function(jq,_d){
if(_d<0){
_d=0;
}
if(_d>100){
_d=100;
}
return jq.each(function(){
var _e=$.data(this,"progressbar").options;
var _f=_e.text.replace(/{value}/,_d);
var _10=_e.value;
_e.value=_d;
$(this).find("div.progressbar-value").width(_d+"%");
$(this).find("div.progressbar-text").html(_f);
if(_10!=_d){
_e.onChange.call(this,_d,_10);
}
});
}};
$.fn.progressbar.parseOptions=function(_11){
var t=$(_11);
return {width:(parseInt(_11.style.width)||undefined),value:(t.attr("value")?parseInt(t.attr("value")):undefined),text:t.attr("text")};
};
$.fn.progressbar.defaults={width:"auto",value:0,text:"{value}%",onChange:function(_12,_13){
}};
})(jQuery);

