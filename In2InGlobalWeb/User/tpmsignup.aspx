<%@ Page Language="C#" AutoEventWireup="true" EnableEventValidation="false" CodeBehind="tpmsignup.aspx.cs" Inherits="kss.ittpm.presentation.tpmsignup" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
<meta http-equiv="X-UA-Compatible" content="IE=9" />
    <title></title>
    <style type="text/css">
    #SignupDiv{
     position:absolute;
     top:13%;
     left:32%;     
     margin:0px;
     vertical-align:middle;
     
}

    </style>
    <link rel="stylesheet" type="text/css" href="../NewJEasyUI/themes/black/easyui.css"/>
    <link rel="stylesheet" type="text/css" href="../NewJEasyUI/themes/icon.css"/>
    <link href="../css/msgBoxLight.css" rel="stylesheet" type="text/css" />
    <script src="../NewJEasyUI/jquery.min.js" type="text/javascript" language="javascript"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            RestrictInput();
        });
        function RestrictInput() {
            $('input').bind('keypress', function (event) {
                var regex = new RegExp("^[a-zA-Z0-9@]+$");
                var key = String.fromCharCode(!event.charCode ? event.which : event.charCode);
                if (!regex.test(key)) {
                    event.preventDefault();
                    return false;
                }
            });
            $('input').bind('keypress', function (event) {
                var regex = new RegExp("^[a-zA-Z0-9@]+$");
                var key = String.fromCharCode(!event.charCode ? event.which : event.charCode);
                if (!regex.test(key)) {
                    event.preventDefault();
                    return false;
                }
            });
        }
    </script>
</head>
<body style="background-image:url('../images/signup.png');background-repeat:no-repeat; color:White">
    <form id="form1" runat="server">
<asp:ScriptManager ID="scriptmgrSignup" EnablePageMethods="true" runat="server"
        LoadScriptsBeforeUI="true">
        <CompositeScript>
            <Scripts>
                <%--<asp:ScriptReference Path="../NewJEasyUI/jquery.min.js"/>--%>
                <asp:ScriptReference Path="../NewJEasyUI/jquery.easyui.min.js" />
                <asp:ScriptReference Path="../jquery/jquery.msgBox.js"/>
                <asp:ScriptReference Path="../scripts/ErrorMessage.js"/>
                <asp:ScriptReference Path="../scripts/Validation.js"/>
            </Scripts>
        </CompositeScript>
    </asp:ScriptManager>
    <asp:UpdatePanel ID="updtPnlSignup" runat="server" 
        UpdateMode="Conditional" ChildrenAsTriggers="true">
        <ContentTemplate>
    <center>
    <div id="SignupDiv">
        <table style="width:405px;" cellpadding="0" cellspacing="0">
            <tr align="center">
                <td style="width:100px;text-align:right;background-color: green; color: yellow;padding-right:5px;
                        border-color: Yellow; border-style: solid;border-width:1px; -webkit-border-radius: 4px; -moz-border-radius: 4px;
                        border-radius: 4px;">Full Name</td>
                <td style="width:10px;text-align:center"><img alt="" style="position:relative;padding-top:3px" src="../images/L2RArrow.png"/></td>
                <td style="width:170px;text-align:left">
                <div style="width:176px;padding:0.5px; background-color:White; border-color:Blue;border-style:solid;-webkit-border-radius: 4px; -moz-border-radius: 4px;  border-radius: 4px;">
                <input id="txtFullName" runat="server" value="" style="width:170px;border-width:0px" type="text" />
                </div>
                </td>
            </tr>
            <tr align="center">
                <td colspan="3" style="height: 5px">
                    
                </td>
            </tr>      
            <tr align="center">
                <td style="width:100px;text-align:right;background-color: green; color: yellow;padding-right:5px;
                        border-color: Yellow; border-style: solid;border-width:1px; -webkit-border-radius: 4px; -moz-border-radius: 4px;
                        border-radius: 4px;">Date of Birth</td>
                <td style="width:10px;text-align:center"><img alt="" style="position:relative;padding-top:3px" src="../images/L2RArrow.png"/></td>
                <td style="width:170px;text-align:left">
                <div style="width:176px;padding:0.5px;background-color:White; border-color:Blue;border-style:solid;-webkit-border-radius: 4px; -moz-border-radius: 4px;  border-radius: 4px;">
                <input id="txtDOB" style="width:175px;border-width:0px;border-color:White" runat="server" class="easyui-datebox"/>
                </div></td>
            </tr>
            <tr align="center">
                <td colspan="3" style="height: 5px">
                    
                </td>
            </tr>            
            <tr align="center">
                <td style="width:100px;text-align:right;background-color: green; color: yellow;padding-right:5px;
                        border-color: Yellow; border-style: solid;border-width:1px; -webkit-border-radius: 4px; -moz-border-radius: 4px;
                        border-radius: 4px;">Company Name</td>
                <td style="width:10px;text-align:center"><img alt="" style="position:relative;padding-top:3px" src="../images/L2RArrow.png"/></td>
                <td style="width:170px;text-align:left">
                <div style="width:176px;padding:0.5px;background-color:White; border-color:Blue;border-style:solid;-webkit-border-radius: 4px; -moz-border-radius: 4px;  border-radius: 4px;">
                
                    <select id="ddlCompanyName" onchange="DisplayOtherField();" style="width: 175px; border-width: 0px;">
                        <option value="">--Select Your Company--</option>
                        <optgroup label="Ahmedabad">
                            <option value="tcs-Ahmedabad">Tata Consultancy Services</option>
                            <option value="other-Ahmedabad">--Other--</option>
                       </optgroup>
                        <optgroup label="Bengaluru">
                            <option value="aditi-Bengaluru">Aditi Technologies</option>
                            <option value="infite-Bengaluru">Infinite Computer Solutions</option>
                            <option value="3iinfo-Bengaluru">3i Infotech</option>
                            <option value="accel-Bengaluru">Accel Frontline Ltd</option>
                            <option value="cts-Bengaluru">Cognizant Technology Solutions</option>
                            <option value="hexa-Bengaluru">Hexaware</option>
                            <option value="hcl-Bengaluru">HCL Technologies</option>
                            <option value="igate-Bengaluru">iGate</option>  
                            <option value="infosys-Bengaluru">Infosys</option>
                            <option value="larsen-Bengaluru">Larsen & Toubro Infotech</option>
                            <option value="satyam-Bengaluru">Mahindra Satyam</option>
                            <option value="microland-Bengaluru">Microland</option>
                            <option value="mindtree-Bengaluru">MindTree</option>
                            <option value="plivo-Bengaluru">Plivo</option>
                            <option value="sasken-Bengaluru">Sasken</option>
                            <option value="tcs-Bengaluru">Tata Consultancy Services</option>
                            <option value="mahindra-Bengaluru">Tech Mahindra</option>
                            <option value="thworks-Bengaluru">ThoughtWorks</option>
                            <option value="tpgsi-Bengaluru">TurningPoint Global Solutions</option>
                            <option value="wipro-Bengaluru">Wipro</option>
                            <option value="mphasis-Bengaluru">MphasiS</option>
                            <option value="tally-Bengaluru">Tally Solutions</option>
                            <option value="other-Benaluru">--Other--</option>
                       </optgroup>
                        <optgroup label="Bhubaneswar">
                            <option value="essar-Bhubaneswar">Essar</option>
                            <option value="fsource-Bhubaneswar">Firstsource</option>
                            <option value="genpact-Bhubaneswar">Genpact</option>
                            <option value="idea-Bhubaneswar">Idea Cellular</option>
                            <option value="infosys-Bhubaneswar">Infosys</option>
                            <option value="satyam-Bhubaneswar">Mahindra Satyam</option>
                            <option value="mindfire-Bhubaneswar">Mindfire Solutions</option>
                            <option value="mindtree-Bhubaneswar">Mindtree</option>
                            <option value="mphasys-Bhubaneswar">MphasiS</option>
                            <option value="niit-Bhubaneswar">NIIT</option>
                            <option value="reliance-Bhubaneswar">Reliance Industries</option>
                            <option value="tcs-Bhubaneswar">Tata Consultancy Services</option>
                            <option value="telcon-Bhubaneswar">Telcon Construction Solutions</option>
                            <option value="other-Bhubaneswar">--Other--</option>
                       </optgroup>
                        <optgroup label="Chandigarh">
                            <option value="infosys-chandigarh">Infosys</option>
                            <option value="mahindra-chandigarh">Tech Mahindra</option>
                            <option value="sufi--chandigarh">Sufi</option>
                            <option value="nvish-chandigarh">NVISH</option>
                            <option value="other-chandigarh">--Other--</option>
                       </optgroup>
                        <optgroup label="Chennai">
                            <option value="accel-chennai">Accel Frontline Ltd</option>
                            <option value="accenture-chennai">Accenture</option>
                            <option value="adrenalin-chennai">Adrenalin eSystems</option>
                            <option value="advisory-chennai">The Advisory Board Company</option>
                            <option value="birlasoft-chennai">BirlaSoft</option>
                            <option value="capgemini-chennai">Capgemini</option>
                            <option value="cts-chennai">Cognizant Technology Solutions</option>
                            <option value="csc-chennai">Computer Sciences Corporation</option>
                            <option value="hexa-chennai">Hexaware</option>
                            <option value="hcl-chennai">HCL Technologies</option>
                            <option value="igate-chennai">iGate</option>
                            <option value="infite-chennai">Infinite Computer Solutions</option>
                            <option value="infosys-chennai">Infosys</option>
                            <option value="larsen-chennai">Larsen & Toubro Infotech</option>
                            <option value="lister-chennai">Lister Technologies</option>
                            <option value="logica-chennai">Logica</option>
                            <option value="satyam-chennai">Mahindra Satyam</option>
                            <option value="mindtree-chennai">MindTree</option>
                            <option value="oracle-chennai">Oracle</option>
                            <option value="redington-chennai">Redington (India) Limited</option>
                            <option value="sasken-chennai">Sasken</option>
                            <option value="syntel-chennai">Syntel</option>
                            <option value="tcs-chennai">Tata Consultancy Services</option>
                            <option value="mahindra-chennai">Tech Mahindra</option>
                            <option value="thworks-chennai">ThoughtWorks</option>
                            <option value="ust--chennai">UST Global</option>
                            <option value="vds--chennai">Verizon Data Services</option>
                            <option value="zoho-chennai">ZOHO Corporation</option>
                            <option value="wipro-chennai">Wipro</option>
                            <option value="kla-chennai">KLA_Tencor</option>
                            <option value="other-chennai">--Other--</option>
                       </optgroup>
                        <optgroup label="Coimbatore">
                            <option value="tcs-coimbatore">Tata Consultancy Services</option>
                            <option value="hcl-coimbatore">HCL Technologies</option>
                            <option value="robert-coimbatore">Robert Bosch India</option>
                            <option value="other-coimbatore">--Other--</option>
                       </optgroup>
                        <optgroup label="Delhi">
                            <option value="accel-delhi">Accel Frontline Ltd</option>
                            <option value="hcl-delhi">HCL Technologies</option>
                            <option value="infosys-delhi">Infosys</option>
                            <option value="kayko-delhi">Kayako</option>                            
                            <option value="niit-delhi">NIIT Technologies</option>
                            <option value="tcs-delhi">Tata Consultancy Services</option>
                            <option value="mahindra-delhi">Tech Mahindra</option>
                            <option value="other-delhi">--Other--</option>
                       </optgroup>
                        <optgroup label="Gandhinagar">
                            <option value="cybage-gandhinagar">Cybage</option>
                            <option value="igate-gandhinagar">iGATE</option>
                            <option value="mahindra-gandhinagar">Tech Mahindra</option>
                            <option value="tcs-gandhinagar">Tata Consultancy Services</option>
                            <option value="reliance--gandhinagar">Reliance Industries</option>
                            <option value="other-gandhinagar">--Other--</option>
                       </optgroup>
                        <optgroup label="Gurgaon">
                            <option value="aricent-gurgaon">Aricent Group</option>
                            <option value="comviva-gurgaon">Comviva</option>
                            <option value="genpact-gurgaon">Genpact</option>
                            <option value="infinite-gurgaon">Infinite Computer Solutions</option>
                            <option value="satyam-gurgaon">Mahindra Satyam</option>
                            <option value="micromax-gurgaon">Micromax Mobile</option>
                            <option value="niit-gurgaon">NIIT Technologies</option>
                            <option value="syntel-gurgaon">Syntel</option>
                            <option value="tcs-gurgaon">Tata Consultancy Services</option>
                            <option value="sapient-gurgaon">Sapient Corporation</option>
                            <option value="wipro-gurgaon">Wipro</option>
                            <option value="other-gurgaon">--Other--</option>
                       </optgroup>
                        <optgroup label="Hyderabad">
                            <option value="3iinfo-hyderabad">3i Infotech Limited</option>
                            <option value="accenture-hyderabad">Accenture</option>
                            <option value="adp-hyderabad">ADP</option>
                            <option value="amazon-hyderabad">Amazon</option>
                            <option value="capgemini-hyderabad">Capgemini</option>
                            <option value="cmc-hyderabad">CMC Limited</option>
                            <option value="citrix-hyderabad">Citrix Systems[2]</option>
                            <option value="cts-hyderabad">Cognizant Technology Solutions</option>
                            <option value="comasso-hyderabad">Computer Associates</option>
                            <option value="csc-hyderabad">Computer Sciences Corporation</option>
                            <option value="cybage-hyderabad">Cybage</option>
                            <option value="deliitee-hyderabad">Deloitte Consulting</option>
                            <option value="deshaw-hyderabad">D. E. Shaw Research</option>
                            <option value="genpact-hyderabad">Genpact</option>
                            <option value="hcl-hyderabad">HCL Technologies</option>
                            <option value="headstrong-hyderabad">Headstrong</option>
                            <option value="hsbcsoft-hyderabad">HSBC Software Technology Centre</option>
                            <option value="igate-hyderabad">iGATE</option>
                            <option value="infinite-hyderabad">Infinite Computer Solutions</option>
                            <option value="infitect-hyderabad">Infotech Enterprises</option>
                            <option value="intergraph-hyderabad">Intergraph</option>
                            <option value="satyam-hyderabad">Mahindra Satyam</option>
                            <option value="microsoft-hyderabad">Microsoft Corporation</option>
                            <option value="mindtree-hyderabad">Mindtree</option>
                            <option value="novartis-hyderabad">Novartis</option>
                            <option value="nttdata-hyderabad">NTT Data</option>
                            <option value="persistent-hyderabad">Persistent Systems</option>
                            <option value="polaris-hyderabad">Polaris Financial Technology Limited</option>
                            <option value="pramati-hyderabad">Pramati Technologies</option>
                            <option value="tcs-hyderabad">Tata Consultancy Services</option>
                            <option value="verison-hyderabad">Verizon Data Services</option>
                            <option value="virtusa-hyderabad">Virtusa</option>
                            <option value="wipro-hyderabad">Wipro</option>
                            <option value="other-hyderabad">--Other--</option>
                       </optgroup>
                        <optgroup label="Jaipur">
                            <option value="infosys-jaipur">Infosys</option>
                            <option value="genpact-jaipur">Genpact</option>
                            <option value="other-jaipur">--Other--</option>
                       </optgroup>
                        <optgroup label="Indore">
                            <option value="csc-indore">Computer Sciences Corporation</option>
                            <option value="impetus-indore">Impetus Technologies</option>
                            <option value="other-indore">--Other--</option>
                       </optgroup>
                        <optgroup label="Kochi">
                            <option value="arbiton-kochi">Arbitron</option>
                            <option value="cts-kochi">Cognizant Technology Solutions</option>
                            <option value="srnst-kochi">Ernst & Young</option>
                            <option value="etisalat-kochi">Etisalat DB Telecom</option>
                            <option value="exl-kochi">EXL Service</option>
                            <option value="ibs-kochi">IBS Software</option>
                            <option value="mobme-kochi">MobME</option>
                            <option value="nest-kochi">NeST Software</option>
                            <option value="outsource-kochi">Outsource Partners International</option>
                            <option value="tcs-kochi">Tata Consultancy Services</option>
                            <option value="ust-kochi">UST Global</option>
                            <option value="xerox-kochi">Xerox ACS</option>
                            <option value="other-kochi">--Other--</option>
                       </optgroup>
                        <optgroup label="Kolkata">
                            <option value="accel-kolkata">Accel Frontline Ltd</option>
                            <option value="accenture-kolkata">Accenture</option>
                            <option value="atos-kolkata">Atos</option>
                            <option value="capgemini-kolkata">Capgemini</option>
                            <option value="cdac-kolkata">C-DAC</option>
                            <option value="cmc-kolkata">CMC Limited</option>
                            <option value="cts-kolkata">Cognizant Technology Solutions</option>
                            <option value="deloitte-kolkata">Deloitte</option>
                            <option value="fsource-kolkata">Firstsource</option>
                            <option value="genpact-kolkata">Genpact</option>
                            <option value="hcl-kolkata">HCL Technologies</option>
                            <option value="itc-kolkata">ITC Infotech</option>
                            <option value="ixiz-kolkata">Ixia</option>
                            <option value="lexmark-kolkata">Lexmark</option>
                            <option value="novell-kolkata">Novell</option>
                            <option value="pwc-kolkata">PwC</option>
                            <option value="simens-kolkata">Siemens</option>
                            <option value="tcs-kolkata">Tata Consultancy Services</option>
                            <option value="mahindra-kolkata">Tech Mahindra</option>
                            <option value="niit-kolkata">NIIT Technologies</option>
                            <option value="simens-kolkata">Nokia Siemens</option>
                            <option value="sankalp-kolkata">Sankalp Semiconductor</option>
                            <option value="wipro-kolkata">Wipro</option>
                            <option value="other-kolkata">--Other--</option>
                       </optgroup>
                        <optgroup label="Lucknow">
                            <option value="tcs-lucknow">Tata Consultancy Services</option>
                            <option value="other-lucknow">--Other--</option>
                       </optgroup>
                        <optgroup label="Patna">
                            <option value="tcs-patna">Tata Consultancy Services</option>
                            <option value="other-patna">--Other--</option>
                       </optgroup>
                        <optgroup label="Manesar">
                            <option value="agilent-manesar">Agilent Technologies</option>
                            <option value="hcl-manesar">HCL Technologies</option>
                            <option value="other-manesar">--Other--</option>
                       </optgroup>
                        <optgroup label="Mangalore">
                           <option value="infosys-mangalore">Infosys</option>
                            <option value="mphasis-mangalore">MphasiS</option>
                            <option value="other-mangalore">--Other--</option>
                       </optgroup>
                        <optgroup label="Mumbai">
                            <option value="3iinfo-mumbai">3i Infotech Ltd</option>
                            <option value="accenture-mumbai">Accenture</option>
                            <option value="adrenalin-mumbai">Adrenalin eSystems</option>
                            <option value="aftek-mumbai">Aftek</option>
                            <option value="megatrends-mumbai">American Megatrends India</option>
                            <option value="anantara-mumbai">Anantara Solutions</option>
                            <option value="aptech-mumbai">Aptech</option>
                            <option value="atom-mumbai">Atom Technologies</option>
                            <option value="brahmavision-mumbai">Brahma Vision Private Limited</option>
                            <option value="cgi-mumbai">CGI Group</option>
                            <option value="capgemini-mumbai">Capgemini</option>
                            <option value="cmc-mumbai">CMC Ltd</option>
                            <option value="cts-mumbai">Cognizant Technology Solutions</option>
                            <option value="cynapse-mumbai">Cynapse</option>
                            <option value="deloitte-mumbai">Deloitte Consulting</option>
                            <option value="datamatics-mumbai">Datamatics Global Services</option>
                            <option value="eclrx-mumbai">Eclerx</option>
                            <option value="factset-mumbai">FactSet Research Systems Inc</option>
                            <option value="geometric-mumbai">Geometric Ltd</option>
                            <option value="hcl-mumbai">HCL Technologies</option>
                            <option value="hexa-mumbai">Hexaware Technologies</option>
                            <option value="igate-mumbai">iGATE</option>
                            <option value="iken-mumbai">Iken Solutions</option>
                            <option value="infrasoft-mumbai">Infrasoft Technologies Ltd</option>
                            <option value="ingram-mumbai">Ingram Micro</option>
                            <option value="intec-mumbai">Intec</option>
                            <option value="jpmorgan-mumbai">JP Morgan Chase</option>
                            <option value="kpit-mumbai">KPIT Cummins[3]</option>
                            <option value="larsen-mumbai">Larsen & Toubro Infotech[4]</option>
                            <option value="lionbridge-mumbai">Lionbridge</option>
                            <option value="satyam-mumbai">Mahindra Satyam</option>
                            <option value="mastek-mumbai">Mastek</option>
                            <option value="melstar-mumbai">Melstar Information Technologies</option>
                            <option value="microsoft-mumbai">Microsoft Corporation</option>                            
                            <option value="mindtree-mumbai">MindTree</option>
                            <option value="mphasis-mumbai">MphasiS</option>
                            <option value="ness-mumbai">Ness Technologies</option>
                            <option value="miit-mumbai">NIIT Technologies</option>
                            <option value="novartis-mumbai">Novartis</option>
                            <option value="nvidia-mumbai">Nvidia Corporation</option>
                            <option value="onward-mumbai">Onward Technologies</option>
                            <option value="oracle-mumbai">Oracle Financial Services</option>
                            <option value="patni-mumbai">Patni Computer Systems</option>
                            <option value="ramco-mumbai">Ramco Systems</option>
                            <option value="redhat-mumbai">Red Hat India</option>
                            <option value="rediff-mumbai">Rediff.com</option>
                            <option value="rolta-mumbai">Rolta India Ltd</option>
                            <option value="sonata-mumbai">Sonata Software</option>
                            <option value="syntel">Syntel</option>
                            <option value="sutheland-mumbai">Sutherland</option>
                            <option value="tcs-mumbai">Tata Consultancy Services</option>
                            <option value="tis-mumbai">Tata Interactive Systems</option>
                            <option value="mahindra-mumbai">Tech Mahindra</option>
                            <option value="tejas-mumbai">Tejas Networks</option>
                            <option value="thirdware-mumbai">Thirdware</option>
                            <option value="wns-mumbai">WNS Global Services</option>
                            <option value="zenith-mumbai">Zenith Computers</option>
                            <option value="other-mumbai">--Other--</option>
                       </optgroup>
                        <optgroup label="Mysore">
                            <option value="infosys-mysore">Infosys Technologies</option>
                            <option value="comat-mysore">Comat Technologies</option>
                            <option value="iflex-mysore">Iflex</option>
                            <option value="larsen-mysore">Larsen & Toubro</option>
                            <option value="other-mysore">--Other--</option>
                       </optgroup>
                        <optgroup label="Nagpur">
                            <option value="persistent-nagpur">Persistent Systems</option>
                            <option value="glogic--nagpur">GlobalLogic</option>
                            <option value="caliber-nagpur">Caliber Point</option>
                            <option value="other-nagpur">--Other--</option>
                       </optgroup>
                        <optgroup label="Nashik">
                           <option value="datamatics-nashik">Datamatics Global Services</option>
                            <option value="wns-nashik">WNS Global Services</option>
                            <option value="other-nashik">--Other--</option>
                       </optgroup>
                        <optgroup label="Noida">
                            <option value="3iinfo-noida">3i Infotech Limited</option>
                            <option value="accenture-noida">Accenture</option>
                            <option value="adobe-noida">Adobe Systems</option>
                            <option value="acs-noida">Affiliated Computer Services</option>
                            <option value="borlasoft-noida">BirlaSoft</option>
                            <option value="candence-noida">Cadence Design Systems</option>
                            <option value="csc-noida">Computer Sciences Corporation</option>
                            <option value="conexant-noida">Conexant Systems</option>
                            <option value="coware-noida">CoWare</option>
                            <option value="fiserv-noida">Fiserv</option>
                            <option value="glogic-noida">GlobalLogic</option>
                            <option value="headstrong-noida">Headstrong</option>
                            <option value="hewitt-noida">Hewitt Associates</option>
                            <option value="hcl-noida">HCL Technologies</option>
                            <option value="igate-noida">iGATE</option>
                            <option value="monhava-noida">Monsoon HAVA</option>
                            <option value="mediatek-noida">MediaTek</option>
                            <option value="mindtree-noida">Mindtree</option>
                            <option value="telecomnet-noida">Telecom Network Solutions</option>
                            <option value="niit-noida">NIIT Technologies</option>
                            <option value="siemens-noida">Nokia Siemens</option>
                            <option value="oracle-noida">Oracle Corporation</option>
                            <option value="patni-noida">Patni Computer Systems</option>
                            <option value="pgms-noida">PGMS Inc.</option>
                            <option value="piyney-noida">Pitney Bowes</option>
                            <option value="rsystems-noida">R Systems Ltd</option>
                            <option value="safenet-noida">SafeNet Inc.</option>
                            <option value="sapient">Sapient Corp</option>
                            <option value="sopra--noida">Sopra Group</option>
                            <option value="stmicro-noida"">STMicroelectronics</option>
                            <option value="synapse-noida">SynapseIndia</option>
                            <option value="synopsys-noida">Synopsys</option>
                            <option value="tcs-noida">Tata Consultancy Services</option>
                            <option value="mahindra-noida">Tech Mahindra</option>
                            <option value="wipro-noida">Wipro</option>
                            <option value="xansa-noida">Xansa</option>
                            <option value="xavient-noida">Xavient</option>
                            <option value="other-noida">--Other--</option>
                       </optgroup>
                        <optgroup label="Puducherry (Formerly Pondicherry) ">
                            <option value="lenovo-puducherry">Lenovo</option>
                            <option value="hcl-puducherry">HCL Technologies</option>
                            <option value="mphasis-puducherry">MphasiS</option>
                            <option value="other-puducherry">--Other--</option>
                       </optgroup>
                        <optgroup label="Pune">
                            <option value="accenture-pune">Accenture</option>
                            <option value="amdocs-pune">Amdocs</option>
                            <option value="adp-pune">ADP</option>
                            <option value="avaya-pune">Avaya</option>
                            <option value="atos-pune">AtoS</option>
                            <option value="bmc-pune">BMC Software</option>
                            <option value="barclays-pune">Barclays Capital</option>
                            <option value="captia-pune">Capita</option>
                            <option value="capgemini-pune">Capgemini</option>
                            <option value="cdac-pune">C-DAC</option>
                            <option value="cts-pune">Cognizant echnology Solutions</option>
                            <option value="cybage-pune">Cybage Software</option>
                            <option value="deloitte-pune">Deloitte</option>
                            <option value="fab-pune">Fab.com</option>
                            <option value="fiserv-pune">Fiserv</option>
                            <option value="fujitsu-pune">Fujitsu</option>
                            <option value="futuregroup-pune">Future Group</option>
                            <option value="geometric-pune">Geometric Limited</option>
                            <option value="glogic-pune">GlobalLogic</option>
                            <option value="hexa-pune">Hexaware Technologies</option>
                            <option value="hsbcsoft-pune">HSBC GLT India</option>
                            <option value="igate-pune">iGate</option>
                            <option value="infosys-pune">Infosys</option>
                            <option value="harbinger">Harbinger Systems</option>
                            <option value="kpit-pune">KPIT Cummins</option>
                            <option value="larsen-pune">Larsen & Toubro Infotech</option>
                            <option value="mindtree-pune">MindTree</option>
                            <option value="mastek-pune">Mastek</option>
                            <option value="mphasis-pune">MphasiS</option>
                            <option value="nttdata-pune">NTT Data</option>
                            <option value="nvidia-pune">Nvidia</option>
                            <option value="persistent-pune">Persistent Systems</option>
                            <option value="quinstreet-pune">QuinStreet</option>
                            <option value="redhat-pune">Red Hat India</option>
                            <option value="sasken-pune">Sasken</option>
                            <option value="sunguard-pune">SunGard</option>
                            <option value="sybase-pune">Sybase</option>
                            <option value="symantec-pune">Symantec</option>
                            <option value="syntel-pune">Syntel</option>
                            <option value="symphony-pune">Symphony</option>
                            <option value="tcs-pune">Tata Consultancy Services</option>
                            <option value="mahindra-pune">Tech Mahindra</option>
                            <option value="teradata-pune">Teradata</option>
                            <option value="tibco-pune">Tibco Software</option>
                            <option value="tieto-pune">Tieto</option>
                            <option value="thworks-pune">ThoughtWorks</option>
                            <option value="zsa-pune">ZS Associates</option>
                            <option value="other-pune">--Other--</option>
                       </optgroup>
                        <optgroup label="Trivandrum">
                            <option value="accenture-trivandrum">Accenture</option>
                            <option value="allianz-trivandrum">Allianz</option>
                            <option value="capgemini-trivandrum">Capgemini</option>
                            <option value="cdac-trivandrum">C-DAC</option>
                            <option value="ernst-trivandrum">Ernst & Young Global</option>
                            <option value="flytxt-trivandrum">Flytxt Mobile Solutions</option>
                            <option value="hcl-trivandrum">HCL Technologies</option>
                            <option value="ibs-trivandrum">IBS Software</option>
                            <option value="infosys-trivandrum">Infosys</option>
                            <option value="itc-trivandrum">ITC Infotech</option>
                            <option value="mckinsey-trivandrum">McKinsey & Co.</option>
                            <option value="nest-trivandrum">NeST Software</option>
                            <option value="oracle-trivandrum">Oracle Corporation</option>
                            <option value="rrd-trivandrum">RR Donnelley</option>
                            <option value="suntec-trivandrum">SunTec Business Solutions</option>
                            <option value="tcs-trivandrum">Tata Consultancy Services</option>
                            <option value="toonz-trivandrum">Toonz India Ltd</option>
                            <option value="ust-trivandrum">UST Global</option>
                            <option value="other-trivandrum">--Other--</option>
                       </optgroup>
                        <optgroup label="Vadodara">
                            <option value="tcs-vadodara">Tata Consultancy Services</option>
                            <option value="investis-vadodara">Investis</option>
                            <option value="other-Vadodara">--Other--</option>
                       </optgroup>
                        <optgroup label="Visakhapatnam">
                            <option value="hsbcsoft-visakhapatnam">HSBC Software Development</option>
                            <option value="sutherland-visakhapatnam">Sutherland</option>
                            <option value="symbosis-visakhapatnam">Symbiosis</option>
                            <option value="satyam-visakhapatnam">Mahindra Satyam</option>
                            <option value="wipro-visakhapatnam">Wipro Technologies</option>
                            <option value="other-visakhapatnam">--Other--</option>
                       </optgroup>
                   </select>
                   <div id="divOtherCompany" style="display:none;width:168px;padding:0.5px;background-color:White; border-color:Blue;border-style:solid;-webkit-border-radius: 4px; -moz-border-radius: 4px;  border-radius: 4px;">
                <input id="txtOtherCompany" runat="server" value="" style="width:165px;border-width:0px;border-color:Blue;border-style:solid;-webkit-border-radius: 4px; -moz-border-radius: 4px;  border-radius: 4px;" type="text" />
                </div>
                    </div>  
                    <!--
                    <input class="easyui-combobox" name="browser" style="width:170px;" data-options="url: 'loc.json',method: 'get',valueField:'value',textField:'text',groupField:'group'"/> -->
                </td>
            </tr>
             <tr align="center">
                <td colspan="3" style="height: 5px">
                    
                </td>
            </tr>
             <tr align="center">
                <td style="width:100px;text-align:right;background-color: green; color: yellow;padding-right:5px;
                        border-color: Yellow; border-style: solid;border-width:1px; -webkit-border-radius: 4px; -moz-border-radius: 4px;
                        border-radius: 4px;">Company Email</td>
                <td style="width:10px;text-align:center"><img alt="" style="position:relative;padding-top:3px" src="../images/L2RArrow.png"/></td>
                <td style="width:170px;text-align:left">
                <div style="width:176px;padding:0.5px;background-color:White; border-color:Blue;border-style:solid;-webkit-border-radius: 4px; -moz-border-radius: 4px;  border-radius: 4px;">
                <input id="txtCompanyEmail" runat="server" value="" style="width:170px;border-width:0px;border-color:Blue;border-style:solid;-webkit-border-radius: 4px; -moz-border-radius: 4px;  border-radius: 4px;" type="text" />
                </div>
                </td>
            </tr>
            <tr align="center">
                <td colspan="3" style="height: 5px">
                    
                </td>
            </tr>
             <tr align="center">
                <td colspan="3" style="height: 20px">
                    <input type="button"
                            style="background-color: black; color: white; width: 60px; border-color: Yellow;
                            border-style: solid; -webkit-border-radius: 4px; -moz-border-radius: 4px; border-radius: 4px;"
                            value="Reset" />&nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:Button ID="cmdSignup" runat="server" style="background-color: black; color: white; width: 60px;
                        border-color: Yellow; border-style: solid; -webkit-border-radius: 4px; -moz-border-radius: 4px;
                        border-radius: 4px;text-align:center" onmousedown = "ErrorWindowTopPosision(event);" OnClientClick="return AddTPM();" OnClick="cmdSignup_Click" Text="Signup" />
                </td>
                
            </tr>
        </table>
        </div>
       
    </center>
    <input type="hidden" runat="server" class="ServerResponse" id="hdnServerResponse" value="" />
    <input runat="server" id="hdnCompanyName" type="hidden" value="" />
    </ContentTemplate></asp:UpdatePanel>
    </form>
    <script type="text/javascript">
        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_initializeRequest(InitializeRequest);
        prm.add_endRequest(EndRequest);
        var postBackElement;
        var xPos, yPos;
        function ErrorWindowTopPosision(event) {
            _ewTop = event.clientY;
            _ewLeft = event.clientX - 200;
            _ewTop = _ewTop - 25; //- 100;

        }
        function InitializeRequest(sender, args) {
            if (prm.get_isInAsyncPostBack()) {
                args.set_cancel(true);
            }
            postBackElement = args.get_postBackElement();

            if (postBackElement.id == "cmdSignup") {


                var parentControl = document.getElementsByTagName("body")[0]; //document.getElementById('divMain');
                StartProcess(parentControl, parentControl.style.top, parentControl.style.left);

            }
        }

        function EndRequest(sender, args) {

            var response = $('#hdnServerResponse').val();
            var parentControl = document.getElementsByTagName("body")[0]; //document.getElementById('divMain');

            EndProcess(parentControl);
            if (postBackElement.id == "cmdSignup") {
                if (response == "next") {

                    $.msgBox({
                        title: "Welcome",
                        content: "<b></br>Welcome to ITTPM family.",
                        type: "info",
                        showButtons: false,
                        opacity: 0.9,
                        autoClose: true
                    });

                    window.parent.$('#btnSignupNext').trigger('click');
                }
                else {

                    ShowError('Invalid login credentials.');
                }

            }


        }
        function ResetSignup() {
            $('#txtUserName').val("");
            $('#txtPassword').val("");
        }
        function StartProcess(elm, top, left) {

            //body = document.getElementsByTagName("body");
            //body.style.background-color = "white";
            _width = elm.offsetWidth;
            _height = elm.offsetHeight;
            _top = elm.offsetTop;
            _left = elm.offsetLeft;
            overlay = document.createElement("div");
            overlay.id = "proccessingDIV";
            overlay.style.width = _width + "px";
            overlay.style.height = "100%"; //_height + "px";
            overlay.style.position = "absolute";
            overlay.style.background = "white";
            overlay.style.top = _top + "px";
            overlay.style.left = _left + "px";
            overlay.align = "center";
            overlay.style.valign = "middle";
            overlay.style.filter = "alpha(opacity=50)";
            overlay.style.opacity = "0.5";
            overlay.style.mozOpacity = "0.5";
            var imageleft = (_width / 2) - 100; // 20;
            var imagetop = (_height / 2) + 10; //70;
            overlay.innerHTML = "<img style='position:absolute;top:" + imagetop + "px;left:" + imageleft + "px;' src=\"../images/processing.gif\" alt=\"\" />";
            //document.getElementsByTagName("body")[0].appendChild(overlay);
            elm.appendChild(overlay);
            alert($("#ddlCompanyName").val());
            $('#hdnCompanyName').val($("#ddlCompanyName").val());

        }
        function EndProcess(perCon) {
            overlay = document.getElementById('proccessingDIV');
            if (overlay) {
                perCon.removeChild(overlay);
            }
        }
        function AddTPM() {

            return ValidateSignup();
        }
        function ValidateSignup() {

            Error_Message = "";
            Error_Count = 1;
            CheckNull($("#txtfullName").val(), TPM_3);
            CheckNull($("[name='txtDOB']").val(), TPM_4);
            CheckNull($("#ddlCompanyName").val(), TPM_5);

            if ($("#ddlCompanyName").val().indexOf('other-') > -1) {
                CheckNull($("#txtOtherCompany").val(), TPM_5);
            }
            CheckNull($("#txtCompanyEmail").val(), TPM_6);
            if (Error_Message != "") {
                ShowError(Error_Message, 55);
                return false;
            }
            else {
                return true;
            }
        }
        function DisplayOtherField() {
            if ($("#ddlCompanyName").val().indexOf('other-') > -1) {
                $('#divOtherCompany').show();
            }
            else {
                $('#divOtherCompany').hide();
            }
        }
        function ShowHidden() { } 
          
    </script>
</body>
</html>
