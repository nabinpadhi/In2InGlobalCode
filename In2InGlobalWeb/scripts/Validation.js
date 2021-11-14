var Error_Message ="";
var _ewTop  = 0;
var _ewLeft = 0;
var Error_Count =1;
//'ctl00_MasterContainer_dgdLocation_ctl07_chkAssetLocations'

function CheckLocationSelected(rs_CheckBoxName,rs_gridName,ri_RowCount,rs_ExcludeCheckBoxId,ms_ErrMsg)
{
	var result = false;
	var chkControl = "";
	
	mi_RowCount = parseInt(ri_RowCount) + 2;		
	for(i=2;i < mi_RowCount ;i++)
	{
	    if(i < 10)
	    {
	        chkControl = "ctl0"+i+"_"+rs_CheckBoxName;
	    }
	    else
	    {
	         chkControl = "ctl"+i+"_"+rs_CheckBoxName;
	    }
	    var elementId = 'ctl00_MasterContainer_'+ rs_gridName +'_' + chkControl;		    	  	 
	    if(elementId != rs_ExcludeCheckBoxId)
	    {
	         
	        if(document.getElementById(elementId).checked)
	        {
	           
	            result = true;   
	            break;
	        }
	    }
	}
	
	if(!result)
	{
		Error_Message= Error_Message + Error_Count + " . " +  ms_ErrMsg + "<br>";
		Error_Count = Error_Count + 1;
		return false;
		  
	}
	else
	{ 
		return true;
	}
	
}
function CheckCheckListSelected(rs_TextBox,rs_CheckBoxName,rs_gridName,ri_RowCount,FildNameToValidate,IsDropDown,ms_ErrMsg)
{
	var result = false;
	var chkControl = "";
	var fieldToValidate = "";
	var fieldTovalidatetext="";
	var mi_RowCount = parseInt(ri_RowCount) + 2;		
	for(i=2;i < mi_RowCount ;i++)
	{
	    if(i < 10)
	    {
	       chkControl = "ctl0"+i+"_"+rs_CheckBoxName;
	       fieldToValidate = "ctl0"+i+"_"+FildNameToValidate;
	        fieldTovalidatetext = "ctl0"+i+"_"+rs_TextBox;
	    }
	    else
	    {
	         chkControl = "ctl"+i+"_"+rs_CheckBoxName;
	      fieldToValidate = "ctl"+i+"_"+FildNameToValidate;
	      fieldTovalidatetext = "ctl"+i+"_"+rs_TextBox;
	    }
	    
	    var elementId = 'ctl00_MasterContainer_'+ rs_gridName +'_' + chkControl;
	    var validateFieldId = 'ctl00_MasterContainer_'+ rs_gridName +'_' + fieldToValidate;
	     var validateFieldIdtext = 'ctl00_MasterContainer_'+ rs_gridName +'_' + fieldTovalidatetext;
	       if(document.getElementById(elementId).checked && document.getElementById(elementId).disabled == false)	  	
	    {     	      
   	       if(IsDropDown == true)
   	       {
   	            
   	           CheckNullDropdown(document.getElementById(validateFieldId).selectedIndex,ms_ErrMsg + (i-1));
   	           if(document.getElementById(validateFieldIdtext).value!="")
	            {
   	            ValidateStrings(document.getElementById(validateFieldIdtext).value,Err_229); 
   	            CheckLength(document.getElementById(validateFieldIdtext).value,500,Err_272);  	           
   	            }
   	       }
   	       else
   	       {
               CheckNull(document.getElementById(validateFieldId).value,ms_ErrMsg + (i-2));
               if(document.getElementById(validateFieldIdtext).value!="")
	            {
	            
   	            ValidateStrings(document.getElementById(validateFieldIdtext).value,Err_229);   	           
   	            CheckLength(document.getElementById(validateFieldIdtext).value,500,Err_272);
   	            }
           }         

	    }	  
	}	
}
function IsDirty(rs_CheckBoxName,rs_gridName,ri_RowCount)
{
	var result = false;
	var chkControl = "";	
	var mi_RowCount = parseInt(ri_RowCount) + 2;		
	for(i=2;i < mi_RowCount ;i++)
	{
	    if(i < 10)
	    {
	       chkControl = "ctl0"+i+"_"+rs_CheckBoxName;	       
	    }
	    else
	    {
	      chkControl = "ctl"+i+"_"+rs_CheckBoxName;	      
	    }
	    
	    var elementId = 'ctl00_MasterContainer_'+ rs_gridName +'_' + chkControl;	    
	    if(document.getElementById(elementId).checked && document.getElementById(elementId).disabled == false)	  	
	    {        
            result = true;
            break;             
	    }	  
	}	
	return result;
}

function CheckAll(rs_ParentCheckBoxName,rs_ChildCheckBoxName,rs_gridName,ri_RowCount)
{
    var chkControl = "";
    var mi_RowCount = parseInt(ri_RowCount) + 2;	
    for(i=2;i < mi_RowCount ;i++)
	{
	    if(i < 10)
	    {
	       chkControl = "ctl0"+i+"_"+rs_ChildCheckBoxName;	     
	    }
	    else
	    {
	         chkControl = "ctl"+i+"_"+rs_ChildCheckBoxName;
	     
	    }
	    
	    var elementId = 'ctl00_MasterContainer_'+ rs_gridName +'_' + chkControl;
	    //var parentCheckBoxId = 'ctl00_MasterContainer_'+ rs_gridName +"_ctl0"+i+"_"+rs_ParentCheckBoxName;
	    
	    if(document.getElementById(rs_ParentCheckBoxName).checked)	  	
	    {       
            if(document.getElementById(elementId).disabled == false)
            {
                document.getElementById(elementId).checked = true;
            }
	    }
	    else
	    {
	        if(document.getElementById(elementId).disabled == false)
            {
                document.getElementById(elementId).checked = false;
            }
	    }	  
	}	
}
function CheckDIsplayorderdata(rs_TextBoxName,rs_gridName,ri_RowCount,ms_ErrMsg)
{
	var result = false;
	var chkControl = "";
	var fieldToValidate = "";
	var mi_RowCount = parseInt(ri_RowCount) + 2;		
	for(i=2;i < mi_RowCount ;i++)
	{
	    if(i < 10)
	    {
	       chkControl = rs_gridName +"_ctl0"+i+"_"+rs_TextBoxName;
	       
	    }
	    else
	    {
	         chkControl = rs_gridName +"_ctl"+i+"_"+rs_TextBoxName;
	    }
         
          CheckNull(document.getElementById(chkControl).value,ms_ErrMsg + (i-1));
         if(document.getElementById(chkControl).value!="")
         {
          CheckInt(document.getElementById(chkControl).value,Err_190);
         }
          CheckRowcount(chkControl,mi_RowCount,Err_191);
	}	
}

function CheckRowcount(rs_controlname,rowcount,ms_ErrMsg)
{
var count=parseInt(rowcount-2);

if(parseInt(document.getElementById(rs_controlname).value)>count)
{
Error_Message= Error_Message + Error_Count + " . " +  ms_ErrMsg+ count  + "<br>";
Error_Count = Error_Count + 1;
return false
}
else
return true
}

function CheckBoxSelected(rs_CheckBoxName,rs_gridName,ri_RowCount,rs_ExcludeCheckBoxId,ms_ErrMsg)
{
	var result = false;
	var chkControl = "ctl03_chkSignOff";
	mi_RowCount = parseInt(ri_RowCount) + 2;		
	
	for(i=2;i < mi_RowCount ;i++)
	{
	    var elementId = 'ctl00_MasterContainer_'+ rs_gridName +'_' + chkControl;		    	  
	    if(elementId != rs_ExcludeCheckBoxId)
	    {   
	        if(document.getElementById(elementId).checked)
	        {
	            result = true;   	          
	            break;
	        }
	        
	    }
	}

	if(!result)
	{
		Error_Message= Error_Message + Error_Count + " . " +  ms_ErrMsg + "<br>";
		Error_Count = Error_Count + 1;
		return false;
	}
	else
	{
		return true;
	}
}


function CheckNull(rs_FieldValueToValidate,ms_ErrMsg)
{	
    if(rs_FieldValueToValidate != null)
    {
    rs_FieldValueToValidate=rs_FieldValueToValidate.toString().trim();
    }
	if(rs_FieldValueToValidate == "" || rs_FieldValueToValidate == null)
	{

		Error_Message= Error_Message +  Error_Count + " . " + ms_ErrMsg + "<br>";
		Error_Count = Error_Count + 1;
		
		return false;
	}
	else
	{
		return true;
	}
}
function CheckNullNSpace(rs_FieldValueToValidate,ms_ErrMsg)
{
	if(Trim(rs_FieldValueToValidate) == "" || rs_FieldValueToValidate == null)
	{
		Error_Message= Error_Message + Error_Count + " . " + ms_ErrMsg + "<br>";
		Error_Count = Error_Count + 1;
		return false;
	}
	else
	{
		return true;
	}
}
function CompareTwoString(rs_FieldValueOne,rs_FieldValueTwo,ms_ErrMsg)
{
	if(rs_FieldValueOne != "")
	{
		
		if(Trim(rs_FieldValueOne)== Trim(rs_FieldValueTwo))
		{
			Error_Message= Error_Message  + Error_Count + " . " + ms_ErrMsg + "<br>";
			Error_Count = Error_Count + 1;
			return false;
		}
		else
		{
			return true;
		}
	}
}

function CheckBoxSelected(rs_FieldValueOne,ms_ErrMsg)
{   
	if((rs_FieldValueOne).checked)
	{
			return true;	
	}

	else
	{
			Error_Message= Error_Message  + Error_Count + " . " + ms_ErrMsg + "<br>";
			Error_Count = Error_Count + 1;
			return false;
	}
}

function ComparePassword(rs_FieldValueOne,rs_FieldValueTwo,ms_ErrMsg)
{
	if(rs_FieldValueOne != "")
	{	
		if(Trim(rs_FieldValueOne)!= Trim(rs_FieldValueTwo))
		{
			Error_Message= Error_Message  + Error_Count + " . " + ms_ErrMsg + "<br>";
			Error_Count = Error_Count + 1;
			return false;
		}
		else
		{
			return true;
		}
	}
}

function CompareTwoDate(rs_FieldValueOne,rs_FieldValueTwo,ms_ErrMsg,type)
{
	if(rs_FieldValueOne != "" && rs_FieldValueTwo != "")
	{
		switch (type)
		{
		 case 1: // If Greater Log error
		  if(Date.parse(rs_FieldValueOne)  >  Date.parse(rs_FieldValueTwo))
		    {
		       Error_Message= Error_Message + Error_Count + " . " + ms_ErrMsg + "<br>";
			    Error_Count = Error_Count + 1;
			    return false;   
		    }
		    else
		    {
			    return true;
		    }
		 break;
		 case 2: // if smaller log error            
		    if(Date.parse(rs_FieldValueOne)  <  Date.parse(rs_FieldValueTwo))
		    {
		       Error_Message= Error_Message + Error_Count + " . " + ms_ErrMsg + "<br>";
			    Error_Count = Error_Count + 1;
			    return false; 
		    }
		    else
		    {
			    return true;
		    }
		 break;
		 case  3: // if equal log error
		  if(Date.parse(rs_FieldValueOne)  ==  Date.parse(rs_FieldValueTwo))
		    {
		       Error_Message= Error_Message + Error_Count + " . " +  ms_ErrMsg + "<br>";
			    Error_Count = Error_Count + 1;
			    return false; 
		    }
		    else
		    {
			    return true;
		    }
		 break;
		 default:
		 break;
		}
	}
}

function CheckNullDropdown(rs_FieldValueToValidate,ms_ErrMsg)
{	
	if(rs_FieldValueToValidate == 0 )
	{
		Error_Message= Error_Message + Error_Count + " . " + ms_ErrMsg + "<br>";
		Error_Count = Error_Count + 1;
		return false;
	}
	else
	{
		return true;
	}
}
function CheckNullListBox(rs_FieldValueToValidate,ms_ErrMsg)
{
	if(rs_FieldValueToValidate == 0 )
	{
		Error_Message= Error_Message + Error_Count + " . " + ms_ErrMsg + "<br>";
		Error_Count = Error_Count + 1;
		return false;
	}
	else
	{
		return true;
	}
}
function MasterCheck()
{
	if(frm.hMasterCheck.value=="False")
	{
		document.getElementById("trAdminTabs").style.display="none";
		document.getElementById("trClientTabs").style.display="";
		document.getElementById("tblMaster").style.display="none";
				
	}
	else
	{
		document.getElementById("trAdminTabs").style.display="";
		document.getElementById("trClientTabs").style.display="none";
		document.getElementById("tblMaster").style.display="";
	}	
}

function CheckAID(ValueToChk,ErrMsg)
{
	if(ValueToChk !="")
	{
		if(CheckInt(ValueToChk,ErrMsg)==true)
		{
			CheckIntLength(ValueToChk,6,ErrMsg);
		}
	}
}

function CheckInt(ValueToCheck,ErrMsg)
{
	var parsedValue = parseInt(ValueToCheck);
	if(ValueToCheck != parsedValue )
	{
		Error_Message= Error_Message + Error_Count + " . " + ErrMsg + "<br>";
		Error_Count = Error_Count + 1;
		return false;
	}
	else
	{
		return true;
	}
}




function ValidateFileExtention(ValueToCheck,ErrMsg,fileExtension)
{
    var splitChar = "\\";    
	var uploadedFileExtension = ValueToCheck.split(splitChar)[ValueToCheck.split(splitChar).length-1].toString().split('.')[1];
	if(uploadedFileExtension != fileExtension )
	{
		Error_Message= Error_Message + Error_Count + " . " + ErrMsg + "<br>";
		Error_Count = Error_Count + 1;
		return false;
	}
	else
	{
		return true;
	}
}
function ValidateFileExtension(ValueToCheck,ErrMsg,fileExtension,fileExtension1,fileExtension2)
{
    var splitChar = "\\";    
	var uploadedFileExtension = ValueToCheck.split(splitChar)[ValueToCheck.split(splitChar).length-1].toString().split('.')[1];
		
	if(uploadedFileExtension != fileExtension || uploadedFileExtension != fileExtension2 || uploadedFileExtension != fileExtension2)
	{
		Error_Message= Error_Message + Error_Count + " . " + ErrMsg + "<br>";
		Error_Count = Error_Count + 1;
		return false;
	}

	
	else
	{
		return true;
	}
}
function ValidatelogoFileExtension(ValueToCheck,ErrMsg)
{
    var splitChar = "\\";    
	var uploadedFileExtension = ValueToCheck.split(splitChar)[ValueToCheck.split(splitChar).length-1].toString().split('.')[1];
	
	
    var filter=/^(gif|jpg|jpeg|png|PNG|JPG|JPEG|GIF)$/
	
	if(filter.test(uploadedFileExtension))
	{
		return true;
	}

	
	else
	{
		
		Error_Message= Error_Message + Error_Count + " . " + ErrMsg + "<br>";
		Error_Count = Error_Count + 1;
		return false;
	}
}

function ValidateDMSFileExtension(ValueToCheck,ErrMsg)
{
    var splitChar = "\\";    
	var uploadedFileExtension = ValueToCheck.split(splitChar)[ValueToCheck.split(splitChar).length-1].toString().split('.')[ValueToCheck.split(splitChar)[ValueToCheck.split(splitChar).length-1].toString().split('.').length-1];
	
    var filter=/^(xls|doc|docx|pdf|xlsx|XLS|DOC|DOCX|PDF|XLSX)$/
	
	if(filter.test(uploadedFileExtension))
	{
		return true;
	}

	
	else
	{
		
		Error_Message= Error_Message + Error_Count + " . " + ErrMsg + "<br>";
		Error_Count = Error_Count + 1;
		return false;
	}
}

function ValidateFileExtension(ValueToCheck,ErrMsg)
{
   
	
	
    var filter=/^(xls|doc|docx|pdf|xlsx|XLS|DOC|DOCX|PDF|XLSX)$/
	
	if(filter.test(ValueToCheck))
	{
		return true;
	}

	
	else
	{
		
		Error_Message= Error_Message + Error_Count + " . " + ErrMsg + "<br>";
		Error_Count = Error_Count + 1;
		return false;
	}
}

function ValidateFileExtension1(ValueToCheck,ErrMsg)
{
   
	
	
    var filter=/^(xls|xlsx|XLS|XLSX)$/
	
	if(filter.test(ValueToCheck))
	{
		return true;
	}

	
	else
	{
		
		Error_Message= Error_Message + Error_Count + " . " + ErrMsg + "<br>";
		Error_Count = Error_Count + 1;
		return false;
	}
}

function CheckIntLength(ValueToChk,length,ErrMsg)
{
	if(ValueToChk.length != length)
	{
		Error_Message= Error_Message + Error_Count + " . " + ErrMsg + "<br>";
		Error_Count = Error_Count + 1;
		return false;
	}
	else
	{
		return true;
	}
}
function CheckLength(ValueToChk,length,ErrMsg)
{
	
	if(ValueToChk.length > length)
	{
		Error_Message= Error_Message + Error_Count + " . " + ErrMsg + "<br>";
		Error_Count = Error_Count + 1;
		return false;
	}
	else
	{
		return true;
	}
}
function fnTrim(string)
{
	var cnt;	
	len = string.length;
	str = string;
	begin = -1;
	for(cnt=0;cnt<len;cnt++)
	{   
		if (str.charAt(cnt) == " " )
		{	
			begin = cnt;
		}	
		else
		break;
	}
	str = str.slice(begin+1,len);
	len = str.length;
	end = len;
	for(cnt=len-1;cnt>=0;cnt--)
	{
		if (str.charAt(cnt) == " ")
			end = cnt;
		else
		break;
	}
	str = str.slice(0,end);
	return str;
}
function Trim ( ro_MessageToTrim ) 
{	
	var mo_RegExp = /\s/g; //Match any white space including space , tab , form-feed , etc. 
	var ms_Str = ro_MessageToTrim.replace ( mo_RegExp , "" ) ;
	return ms_Str;		
}
function ValidateEmail(rs_ValueToValidate,ErrMsg)
{
    var filter=/^([\w-]+(?:\.[\w-]+)*)@((?:[\w-]+\.)*\w[\w-]{0,66})\.([a-z]{2,6}(?:\.[a-z]{2})?)$/i
 
   var filter1=/^([a-zA-Z0-9]+[a-zA-Z0-9_\-\.]+)@((([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4})$/
   
   
    if (filter1.test(rs_ValueToValidate))
    {
        return true;
    }
    else
    {
        Error_Message= Error_Message + Error_Count + " . " + ErrMsg + "<br>";
		Error_Count = Error_Count + 1;
		return false;
    } 
}
function ValidateName(rs_ValueToValidate,ErrMsg)
{  
    var filter=/^\s*[a-zA-Z,.,(,)\s]+\s*$/
    if (filter.test(rs_ValueToValidate))
    {
        return true;
    }
    else
    {
        Error_Message= Error_Message  +  Error_Count + " . " + ErrMsg + "<br>";
		Error_Count = Error_Count + 1;
		return false;
    } 
}
function ValidateCompanyName(rs_ValueToValidate,ErrMsg)
{  
    var filter=/^\s*[0-9][a-zA-Z,-,_,.,(,)\s]+\s*$/
    if (filter.test(rs_ValueToValidate))
    {
        return true;
    }
    else
    {
        Error_Message= Error_Message  +  Error_Count + " . " + ErrMsg + "<br>";
		Error_Count = Error_Count + 1;
		return false;
    } 
}



function validatescripts(rs_ValueToValidate,ErrMsg)
{
 var filter =/^(?![<])*$/

if (filter.test(rs_ValueToValidate))
    {
        return true;
    }
    else
    {
        Error_Message= Error_Message  + Error_Count + " . " + ErrMsg + "<br>";
		Error_Count = Error_Count + 1;
		return false;
    } 
}

function ValidateNumber(rs_ValueToValidate,ErrMsg)
{

var filter=/^([+]?\d{1,6}[-\s]?\d{2,15})*$/
 if (filter.test(rs_ValueToValidate))
    {
        return true;
    }
    else
    {
        Error_Message= Error_Message  +  Error_Count + " . " + ErrMsg + "<br>";
		Error_Count = Error_Count + 1;
		return false;
    } 
}
function ValidatePhoneNumber(rs_ValueToValidate, ErrMsg) {

    var filter = /^(?:(?:\+|0{0,2})91(\s*[\-]\s*)?|[0]?)?[789]\d{9}$/
    if (filter.test(rs_ValueToValidate)) {
        return true;
    }
    else {
        Error_Message = Error_Message + Error_Count + " . " + ErrMsg + "<br>";
        Error_Count = Error_Count + 1;
        return false;
    }
}

function ValidateZipcode(rs_ValueToValidate,ErrMsg)
{
var filter=/^(\d{5,6})*$/
 if (filter.test(rs_ValueToValidate))
    {
        return true;
    }
    else
    {
        Error_Message= Error_Message  + Error_Count + " . " + ErrMsg + "<br>";
		Error_Count = Error_Count + 1;
		return false;
    } 
}
function ValidateNumber(rs_ValueToValidate,ErrMsg)
{
var filter=/^(\d{1,2})*$/
 if (filter.test(rs_ValueToValidate))
    {
        return true;
    }
    else
    {
        Error_Message= Error_Message  + Error_Count + " . " + ErrMsg + "<br>";
		Error_Count = Error_Count + 1;
		return false;
    } 
}



function ValidatePassword(rs_ValueToValidate,ErrMsg)
{
//var filter=/(?=^.{10,15}$)(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[!@#$%^&amp;*()_+}{&quot;:;'?/&gt;.&lt;,])(?!.*\s).*$/
var filter = /^.*(?=.{8,15})(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[!@#$%^&*]).*$/
//var filter=/^(?!^[0-9]*$)(?!^[a-zA-Z]*$)^([a-zA-Z0-9]{7,15})$/
if(filter.test(rs_ValueToValidate))
{
return true;
}
else
{
 Error_Message= Error_Message  + Error_Count + " . " + ErrMsg  + "<br>";
 Error_Count = Error_Count + 1;
 return false;
}
}

function IsValidData(value,coloumn)
{
    
}
function ValidateStrings(rs_ValueToValidate,ErrMsg)
{

var filter=/^\s*[a-zA-Z0-9-,.,?,&,#,(,),',",:,[,/,@,+,%,{,},$,=^,_\s\]]+\s*$/;
//var filter=/^\s*[[^<>]\s]+\s*$/;

if(filter.test(rs_ValueToValidate))
{
return true;
}
else
{
 Error_Message= Error_Message  + Error_Count + " . " + ErrMsg  + "<br>";
 Error_Count = Error_Count + 1;
 return false;
}
}

function ValidateURL(rs_ValueToValidate,ErrMsg)
{


var filter=/^(ftp|https?):\/\/+(www\.)?[a-z0-9\-\.]{3,}\.[a-z]{2,3}$/;

if(filter.test(rs_ValueToValidate))
{
return true;
}
else
{
 Error_Message= Error_Message  + Error_Count + " . " + ErrMsg  + "<br>";
 Error_Count = Error_Count + 1;
 return false;
}
}


function Validateprivatemail(rs_ValueToValidate,ErrMsg)
{
if(rs_ValueToValidate.indexOf("gmail")==-1&& rs_ValueToValidate.indexOf("yahoomail")==-1&&rs_ValueToValidate.indexOf("rediffmail")==-1&&rs_ValueToValidate.indexOf("live")==-1&&rs_ValueToValidate.indexOf("indiatimes")==-1&&rs_ValueToValidate.indexOf("yahoo")==-1)

{
return true;
}
else
{
 Error_Message= Error_Message  + Error_Count + " . " + ErrMsg  + "<br>";
 Error_Count = Error_Count + 1;
 return false;

}
}

function ValidateText(rs_ValueToValidate,ErrMsg)
{
var filter=/^[a-zA-Z\s]+[a-zA-Z0-9-_().\s]*$/
 if (filter.test(rs_ValueToValidate))
    {
        return true;
    }
    else
    {
        Error_Message= Error_Message  +  Error_Count + " . " + ErrMsg + "<br>";
		Error_Count = Error_Count + 1;
		return false;
    } 
}
function xtractFile(data){
				data = data.replace(/^\s|\s$/g, "");
				if (/\.\w+$/.test(data)) {
					if (data.match(/([^\/\\]+)\.(\w+)$/) )
						return {file: RegExp.$1, ext: RegExp.$2};
					else
						return {file: "no file name", ext: null};
				} else {
					if (data.match(/([^\/\\]+)$/) )
						return {file: RegExp.$1, ext: null};
					else
						return {file: "no file name", ext: null};
				}
			
 
		}


function ValidateInput(eventInstance) {
	eventInstance = eventInstance || window.event;
	key = eventInstance.keyCode || eventInstance.which;
	if ((key > 47 && key < 58) || (key == 45 || key == 8)) {
		return true;
	} else {
		if (eventInstance.preventDefault) eventInstance.preventDefault();
		eventInstance.returnValue = false;
		return false;
	} //if
}

function ShowError(rs_ErrorMessage,messageHeight)
{
    if(_ewLeft == 0)
    {

            $.messager.show({
		    title:'In2In Global - Errors',
		    msg:rs_ErrorMessage,
		    showType:'slide',
		    style:{
			    right:'',                 
			    top:'' ,
			    bottom: -document.body.scrollTop-document.documentElement.scrollTop
		    }
	    });
    }
    else
    {
         $.messager.show({
		    title:'In2In Global - Errors',
		    msg:rs_ErrorMessage,
		    showType:'slide',
		    style:{
			    right:'', 
                left: _ewLeft,
			    top:'' ,
			    bottom: _ewTop //-document.body.scrollTop-document.documentElement.scrollTop
		    }
	    });
    }
            if(messageHeight != "")
            {
                $('.messager-body.panel-body.panel-body-noborder.window-body').height(messageHeight + 'px');
            }
            $('.panel-title').css('color','Red');
                
}
function CheckSaveModal()
{  
  if(document.getElementById('hdnsavevalue') != null && document.getElementById('hdnsavevalue').value != "")
    {
        document.getElementById("divmessage").style.display="inline";        
        $("#divmessage").text($('#hdnsavevalue').val());        
        document.getElementById('hdnsavevalue').value = '';
        saveMessageTimer = setInterval(ClearSaveMessage,2500); 
    }
    else
    {
        if(document.getElementById("divmessage"))
        {
         document.getElementById("divmessage").style.display="none";
      }
        
    }
     
}
var saveMessageTimer;
function ClearSaveMessage()
{   
       document.getElementById("divmessage").style.display="none";        
        document.getElementById("divmessage").innerText ='';        
      clearInterval(saveMessageTimer); 
     

}
/* Non Validation Methods , included to save time 0 as this script file has been imported in every input form screen*/
function AddCSSToTextBox()
{
    var inputs=document.getElementsByTagName('input');
    for(i=0;i<inputs.length;i++)
    {
     if(inputs[i].type.match(/text/i)){
     if(inputs[i].id != "txtSearchText_t")
     {
         inputs[i].style.width="98%";
         inputs[i].className='singleline-textbox-noborder';
         var childBackup = inputs[i];     
         var oldParent = childBackup.parentNode;
         var newParent = document.createElement('div'); 
         newParent.id = "div" + childBackup.id;
         newParent.className = "roundedCorners";
         newParent.style.width = "80%";
         newParent.style.height ="20px";
         newParent.appendChild(childBackup);
         //oldParent.removeChild(inputs[i]);
         oldParent.appendChild(newParent);
      }   
     }
  }
}

var browserName = navigator.appName.toLowerCase(); 
var isConnerScriptRequired = false;
if (browserName == "microsoft internet explorer" || browserName == "opera") { 
isConnerScriptRequired = true;
}
function RemoveLineBreaks(val){
  
    var noBreaksText =val;
    noBreaksText = noBreaksText.replace(/(\r\n|\n|\r)/gm,"<1br />");

    re1 = /<1br \/><1br \/>/gi;
    re1a = /<1br \/><1br \/><1br \/>/gi;

  
    noBreaksText = noBreaksText.replace(re1a,"<1br /><2br />");
    noBreaksText = noBreaksText.replace(re1,"<2br />");
  
    re2 = /\<1br \/>/gi;
    noBreaksText = noBreaksText.replace(re2, " ");

    re3 = /\s+/g;
    noBreaksText = noBreaksText.replace(re3," ");

    re4 = /<2br \/>/gi;
    noBreaksText = noBreaksText.replace(re4,"\n\n");
    return noBreaksText;
}

  function preventSearchTextEnterKeyPress(event) {
           var keyCode = event.keyCode ? event.keyCode : event.which ? event.which : event.charCode;
           if (keyCode == 13) {


               if (!e) var e = window.event;

               e.cancelBubble = true;
               e.returnValue = false;

               if (e.stopPropagation) {
                   e.stopPropagation();
                   e.preventDefault();
               }
           }
       }



function excludeCharacters(failureMessage)
{
        B.negate = true;
        Validate.Inclusion(A, B);
        return true;
}

function GetParameterValues(param) {
            var url = window.location.href.slice(window.location.href.indexOf('?') + 1).split('&');
            for (var i = 0; i < url.length; i++) {
                var urlparam = url[i].split('=');
                if (urlparam[0] == param) {
                    return urlparam[1];
                }
            }
        }