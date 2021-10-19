	$.userClickF = function(obj) { 
		$(obj.element).hover();
		return false;
	}
	$.userGridCellClick = function(cellid) {
        var cellId=cellid.element.substring(1);
        var cell=igtbl_getCellById(cellId);
        cell.beginEdit();
		return false;
	}

	$.userValueF = function(obj) { 
		$(obj.element).val(obj.value);
		return false;
	}
    $.userTClickF = function(obj) { 
		$(obj.element).click();
	}	 
	function AddAssetTabClick(obj) 
	{ 
	    if($(obj).val() == "Add New Asset")
	    {
		    $(obj).click();
		}
		else
		{
		    return false;
		}
	}
	function AddVulnerabilityTabClick(obj) 
	{ 
	    if($(obj).val() == "Add New Vulnerability")
	    {
		    $(obj).click();
		}
		else
		{
		    return false;
		}
	}
	function AddThreatTabClick(obj) 
	{ 
	    if($(obj).val() == "Add New Threat")
	    {
		    $(obj).click();
		}
		else
		{
		    return false;
		}
	}
    function userOpenDropDownF(dpElement) {         	
		$(dpElement).multiselect("open");
		$(dpElement).multiselect("close");
		return false;
	}
	 $.userMultiDropDownF = function(obj) { 
		$(obj.element).multiselect("open");
		$(obj.element).multiselect("checkall");
		$(obj.element).multiselect("uncheckall");
		$(obj.element).multiselect("close");
		return false;
	}
	
	function diferentWay(element, txt){
		$(element).val(txt);
		return false;
	} 
	function CloseHelp()
    {
        $("#rightdiv").fadeOut('slow', function() {
                        // Animation complete
                         });
    }