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
var _3=$.data(_2,"numberspinner").options;
$(_2).spinner(_3).numberbox(_3);
};
function _4(_5,_6){
var _7=$.data(_5,"numberspinner").options;
var v=parseFloat($(_5).numberbox("getValue")||_7.value)||0;
if(_6==true){
v-=_7.increment;
}else{
v+=_7.increment;
}
$(_5).numberbox("setValue",v);
};
$.fn.numberspinner=function(_8,_9){
if(typeof _8=="string"){
var _a=$.fn.numberspinner.methods[_8];
if(_a){
return _a(this,_9);
}else{
return this.spinner(_8,_9);
}
}
_8=_8||{};
return this.each(function(){
var _b=$.data(this,"numberspinner");
if(_b){
$.extend(_b.options,_8);
}else{
$.data(this,"numberspinner",{options:$.extend({},$.fn.numberspinner.defaults,$.fn.numberspinner.parseOptions(this),_8)});
}
_1(this);
});
};
$.fn.numberspinner.methods={options:function(jq){
var _c=$.data(jq[0],"numberspinner").options;
return $.extend(_c,{value:jq.numberbox("getValue")});
},setValue:function(jq,_d){
return jq.each(function(){
$(this).numberbox("setValue",_d);
});
},getValue:function(jq){
return jq.numberbox("getValue");
},clear:function(jq){
return jq.each(function(){
$(this).spinner("clear");
$(this).numberbox("clear");
});
}};
$.fn.numberspinner.parseOptions=function(_e){
return $.extend({},$.fn.spinner.parseOptions(_e),$.fn.numberbox.parseOptions(_e),{});
};
$.fn.numberspinner.defaults=$.extend({},$.fn.spinner.defaults,$.fn.numberbox.defaults,{spin:function(_f){
_4(this,_f);
}});
})(jQuery);

