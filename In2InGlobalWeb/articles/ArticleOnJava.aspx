<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ArticleOnJava.aspx.cs" Inherits="InGlobal.presentation.blog.ArticleOnJava" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
       <style type="text/css">

        .blogTitle {
        font-size: 250%;
        font-weight: bold;
        color: #471654;
        }
        .about-denis-pilipchuk {
        width: 100%;
        padding-left: 25px;
        padding-right: 25px;
        box-sizing: border-box;
        -moz-box-sizing: border-box;
       
        padding-top: 1em;
        padding-bottom: 1em;
        margin-bottom: 1.5em;
        position: relative;
        overflow: hidden;
        }
        .bioBox {
        width: 360px;
        float: left;
        }
        .bioBoxInner {
        padding-left: 85px;
        background: transparent url('image/Denis-Pilipchuk.jpg') no-repeat left top;
        }
        hr {
            
border-color: #e2842c;
color: #e2842c;
}
    </style>
    </style>
</head>
<body>    
    <form id="form1" runat="server">
       
          <div style="background-color: #f6f6ea;border-radius:25px;width:100%;">
                     <div style="width:98%;height:530px;overflow-y:auto">
                <table style="width:98%" cellspacing="10px">

                    <tr><td colspan="2" align="center">
                        <h1 class="blogTitle">Java SE Access Control model</h1>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <p>The Java SE Access Control model, built around various permission classes and code-based security, has not been able to grow up with the Java platform and cannot satisfy the requirements of today's enterprise systems. This article analyzes the root causes of the problem, and suggests alternative approaches.</p>
                    <h3 id="Model">Brief Overview of the Java SE Model</h3>
                    <p>The model is based on using permission classes from the code to assert the right to perform some action. When certain action is about to be executed in a particular class/page/etc., the code invokes <a href="http://java.sun.com/javase/6/docs/api/java/lang/SecurityManager.html" target="_blank"><code class="prettyprint"><span class="typ">SecurityManager</span></code></a>'s (or <a href="http://java.sun.com/javase/6/docs/api/java/security/AccessController.html" target="_blank"><code class="prettyprint"><span class="typ">AccessController</span></code></a>'s) method <a href="http://java.sun.com/javase/6/docs/api/java/lang/SecurityManager.html#checkPermission(java.security.Permission)" target="_blank"><code class="prettyprint"><span class="pln">checkPermission</span><span class="pun">(</span><span class="typ">Permission</span><span class="pun">)</span></code></a>, passing it an instance of some class derived from the base <a href="http://java.sun.com/javase/6/docs/api/java/security/Permission.html" target="_blank"><code class="prettyprint"><span class="typ">Permission</span></code></a> class. The <a href="http://java.sun.com/javase/6/docs/api/java/security/AccessController.html" target="_blank"><code class="prettyprint"><span class="typ">AccessController</span></code></a> is then responsible for iterating over the call stack, verifying that each frame on the stack contains the desired permission. Figure 1 shows this arrangement.</p>
                   
                        </td>
                        <td style="width:200px">
                            <div class="about-denis-pilipchuk">
	                            <div class="bioBox">
	                                <h4>About Denis Pilipchuk</h4>
                                    <div class="bioBoxInner">
			                             <p>                        
                            <b>Denis Pilipchuk,</b>has been occupying senior engineering and architectural roles in a number of leading consulting and security companies. Presently he is an architect on the Oracle Entitlements Server team, participates in OASIS WSS and WS-I BSP standards committees, and regularly contributes to industry publications. Denis holds M.S. in Computer Science.
                        </p>

                                    </div>
	                            </div>     
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <p>
                        <img width="451" height="235" alt="Stack walk during checkPermission call" src="image/StackWalk.gif"><br>
                        <i>Figure 1. Stack walk during <code class="prettyprint"><span class="pln">checkPermission</span></code> call</i>
                    </p>
                    <p>It is important to understand that in this model it is the permission class itself that is responsible for executing the evaluation logic, as well as defining the resource structure and available privileges. Permissions for each code frame are derived from those of the executing Java <a href="http://java.sun.com/javase/6/docs/api/java/security/Principal.html" target="_blank"><code class="prettyprint"><span class="typ">Principal</span></code></a>(s) for the current thread and its <a href="http://java.sun.com/javase/6/docs/api/java/security/CodeSource.html" target="_blank"><code class="prettyprint"><span class="typ">CodeSource</span></code></a>. They are calculated by the installed <a href="http://java.sun.com/javase/6/docs/api/java/security/Policy.html" target="_blank"><code class="prettyprint"><span class="pln"></span><span class="typ">Policy</span></code></a> provider, for each code frame on the stack, and the provider is responsible for invoking evaluation logic for each permission class in the resultant set to determine whether the sought permission is implied in it.</p>
                    <p>When successful, the overall <a href="http://java.sun.com/javase/6/docs/api/java/lang/SecurityManager.html#checkPermission(java.security.Permission)" target="_blank"><code class="prettyprint"><span class="pln">checkPermission</span><span class="pun">(</span><span class="typ">Permission</span><span class="pun">)</span></code></a> call either simply returns, or otherwise throws a <a href="http://java.sun.com/javase/6/docs/api/java/lang/SecurityException.html" target="_blank"><code class="prettyprint"><span class="typ">SecurityException</span></code></a> to indicate a security violation.</p>
                    <h3 id="History">History Behind the Java SE Model</h3>
                    <p>Java's security model traces its roots to the early days of the platform, as it was mainly viewed as a browser extension mechanism for enhancing user experience. The Java code for execution would be derived from various sources, some of them of unknown or untrustworthy origins. Correspondingly, the main focus of the platform's security was initially aimed at solving the problem of verifying that the executing code can be trusted, and the entire game revolved around execution of applets in browsers. However, the simple division into <code class="prettyprint"><span class="pln">trusted</span></code> and <code class="prettyprint"><span class="pln">untrusted</span></code> universes that this model dictated was not sufficient for running even moderately complicated applications.</p>
                    <p>Starting with the 1.2 release, as Java was getting accepted as a programming platform and not simply a browser extension, Sun began introducing more flexible security features, the first being the notion of a configurable security policy. Its evolution is covered in the official <a href="http://java.sun.com/javase/6/docs/technotes/guides/security/spec/security-spec.doc1.html" target="_blank"><code class="prettyprint"><span class="typ">Java</span><span class="pln"> documentation</span></code></a>.</p>
                    <p>When the Java platform started making inroads into enterprise environments, it quickly became obvious that purely code-based features are not sufficient to manage security in large-scale applications. Version 1.4 of the Java platform introduced a new feature, called <a href="http://java.sun.com/javase/6/docs/technotes/guides/security/jgss/tutorials/AcnAndAzn.html" target="_blank"><code class="prettyprint"><span class="typ">Java</span><span class="pln"> </span><span class="typ">Authentication</span><span class="pln"> </span><span class="kwd">and</span><span class="pln"> </span><span class="typ">Authorization</span><span class="pln"> </span><span class="typ">Service</span><span class="pln"> </span><span class="pun">(</span><span class="pln">JAAS</span><span class="pun">)</span></code></a>, for incorporating user-based permission entries into the security policy. Now, the permissions for a particular code frame on the stack are based both on the code's origin (its <a href="http://java.sun.com/javase/6/docs/api/java/security/CodeSource.html" target="_blank"><code class="prettyprint"><span class="typ">CodeSource</span></code></a>), as well as the user's identity, groups, and roles, assigned to him during authentication.</p>
                    <h3 id="Problems">Today's Problems</h3>
                    <p>Even after such a long evolution, Java's security model is still best-suited for code access security. However, this model simply fails to address the needs of higher-level enterprise applications, which demand many more access control features than simple permissions checks are capable of performing. Some of these problems are reviewed in the sections below.</p>
                    <h5 id="Management">Management Functionality</h5>
                    <p>Arguably, when it comes to managing security in general and access control in particular, the single greatest obstacle is the absence of a straightforward, yet powerful management model. The list below is intended to highlight the most obvious shortcomings of Java's permission-based model.</p>
                    <ul>
                        <li>
                            <p>
                                <a id="PermProliferation" name="PermProliferation"><b>Permissions Proliferation</b></a><br>
                                The Java platform comes with a number of standard permissions, which are invoked by its own code (see <a href="http://java.sun.com/javase/6/docs/api/java/security/Permission.html" target="_blank"><code class="prettyprint"><span class="typ">Permission</span></code></a>, and its <a href="http://java.sun.com/javase/6/docs/technotes/guides/security/permissions.html#PermRisks" target="_blank"><code class="prettyprint"><span class="pln">subclasses</span></code></a>). However, those permissions are not applicable for an application that tries to protect its own components, so the application has to define its own classes, and quite a few of them.
                            </p>
                            <p>For example, consider a page of a workflow application, as seen in Figure 2:</p>
                            <p>
                                <img width="559" height="282" alt="UI screen for access control" src="image/UIScreen.png"><br>
                                <i>Figure 2. UI screen for access control</i>
                            </p>
                            <p>Not counting standard SE and EE permissions, which are invoked by the application and container (but still need to be managed in the policy), this page would need to define a few separate permissions. First, the application needs to define a specialized permission (call it <code class="prettyprint"><span class="typ">TransferPermission</span></code>) to verify that the user is authorized to perform this step of the workflow process. Second, it checks whether the user can see and use text boxes and buttons on this screen, which would require several additional UI permissions (call them <code class="prettyprint"><span class="typ">AccountEditButtonUIPermission</span></code>, <code class="prettyprint"><span class="typ">AccountTextUIPermission</span></code>, and <code class="prettyprint"><span class="typ">AmountTextUIPermission</span></code>). Third, some of the data on the page, such as the account number, might need to be hidden; unfortunately, Java does not provide any mechanism to address that need (see the <a href="#DataSec">Consistency of Data and Function Security</a> section).</p>
                            <p>Considering that large-scale applications (especially in the financial and healthcare industries) typically have thousands of elements, this quickly pushes the total number of defined permissions to hundreds or even thousands, making the entire set simply unmanageable.</p>
                        </li>
                        <li><b>Resource Model</b>
                            <p>Each permission, regardless of whether it is standard or custom, incorporates its own resource interpretation logic into the evaluation process. Which means that permission classes operate on different representations of application resources, making it impossible for security administrators to create a coherent application management picture of the type shown in Figure 3.</p>
                            <p>
                                <img width="237" height="235" alt="Resources hierarchy" src="image/Res.png"><br>
                                <i>Figure 3. Resources hierarchy</i>
                            </p>
                            <p>For instance, consider Java's standard <a href="http://java.sun.com/javase/6/docs/api/java/io/FilePermission.html" target="_blank"><code class="prettyprint"><span class="typ">FilePermission</span></code> and</a> <a href="http://java.sun.com/javase/6/docs/api/java/net/SocketPermission.html" target="_blank"><code class="prettyprint"><span class="typ">SocketPermission</span></code></a>. The former operates on resource strings, representing files and directories, in the format  <code class="prettyprint"><span class="str">"/MyDir1/MyDir2/*"</span></code>, while the latter uses hosts, sockets, and ports: <code class="prettyprint"><span class="str">"localhost:1024-"</span></code>. In the general case, such differences make it impossible for any administration application to parse the resource strings used by arbitrary permission classes in an application.</p>
                        </li>
                        <li><b>Policy Hierarchies</b>
                            <p>Closely related to the resource model is the absence of policy hierarchies in the Java permission model. A policy hierarchy means that policies, defined at a higher level in the application's resource tree, are applicable to the lower nodes as well, according to well-defined combining algorithms. The advantage here is that a security administrator can define access control policies at the higher levels in hierarchy, and they will be applicable to all applications/domains under his command.</p>
                            <p>Considering the hierarchy from Figure 3, the administrator could restrict access of traders to the trading application, as well as which actions that they can perform, for certain periods of time, in accordance with the company's policy. The permission-based model simply does not allow this level of control, since it does not have a notion of resource hierarchies, and its only policy-combining mechanism consists in creating a union of applicable <a href="http://java.sun.com/javase/6/docs/technotes/guides/security/spec/security-spec.doc3.html#20128" target="_blank">grant entries</a> from all policies.</p>
                        </li>
                        <li>
                            <p><a id="Elements" name="Elements"><b>Policy Elements</b></a></p>
                            <p>Another important aspect of access control is in the capabilities of the security policy itself. Java's policy is very primitive -- it consists of permission sets in <a href="http://java.sun.com/javase/6/docs/technotes/guides/security/spec/security-spec.doc3.html#20128" target="_blank"><code class="prettyprint"><span class="pln">grant entries</span></code></a>, assigned by <a href="http://java.sun.com/javase/6/docs/technotes/guides/security/spec/security-spec.doc3.html#20233" target="_blank"><code class="prettyprint"><span class="typ">Principals</span></code></a>, code origin, or code signer (combined into <a href="http://java.sun.com/javase/6/docs/api/java/security/CodeSource.html" target="_blank"><code class="prettyprint"><span class="typ">CodeSource</span></code></a>).</p>
                            <p>This model lacks the following very important features of authorization policy:</p>
                            <ul>
                                <li>
                                    <p><b>An Ability to Configure <code class="prettyprint"><span class="pln">DENY</span></code> Policies</b></p>
                                    <p>Java treats the absence of an explicit <code class="prettyprint"><span class="pln">GRANT</span></code> as an implicit <code class="prettyprint"><span class="pln">DENY</span></code>. However, given the absence of a uniform resource model, in order to deny access to a particular resource for a user or group, a security administrator has to go through all of the grant entries in the policy to verify that none of them grants access. In the general case, this is an open-ended task, which has to be repeated upon each policy modification. A much easier alternative would be defining a <code class="prettyprint"><span class="pln">DENY</span></code> policy at the top of the application hierarchy, and specifying that a  <code class="prettyprint"><span class="pln">DENY</span></code> result takes precedence.</p>
                                </li>
                                <li>
                                    <p><b>An Ability to Configure Attribute-Based Constraints</b></p>
                                    <p>The policy itself specifies that a particular user (member of some groups and in certain roles), or code (of particular origin or signature) has certain privileges on a particular resource (for instance, <code class="prettyprint"><a href="http://java.sun.com/javase/6/docs/api/java/io/FilePermission.html" target="_blank"><span class="pln"></span><span class="typ">FilePermission</span></a><span class="pun">(</span><span class="str">"/app/user/US/MA/*"</span><span class="pun">,</span><span class="pln"> </span><span class="str">"read"</span><span class="pun">)</span></code> assigns the  privilege <code class="prettyprint"><span class="pln">read</span></code> to the resource <code class="prettyprint"><span class="str">"/app/user/US/MA/*"</span></code>). This means that this file permission would have to be granted to all Massachusetts-based users, groups, or roles that require access to that area, and removed when they do not need it any longer (for example, if they moved to a New York-based office). The same goes for California-based employees, and so on.</p>
                                    <p>It is the attribute-based constraints (aka "conditions") that provide real flexibility in applying the authorization policies. Those constraints allow including external metadata to control when and how a policy's decision can be applied. The real flexibility comes from where and how this metadata can be fetched from: directories (such as LDAP), environment (for example, the OS), or provided with the actual request to the application.</p>
                                    <p>Going back to the earlier example, a single attribute-based policy, applicable to all users and locations across the company, should be defined instead of granting file permissions to multiple user groups:</p>
                                    <code class="prettyprint"><span class="pln">GRANT </span><span class="pun">{</span><span class="typ">User</span><span class="pun">,</span><span class="pln"> </span><span class="typ">Resource</span><span class="pun">,</span><span class="pln"> </span><span class="str">"read"</span><span class="pun">}</span><span class="pln"> </span><b><span class="pln">IF </span><span class="pun">(</span><span class="typ">User</span><span class="pun">.</span><span class="pln">location IN </span><span class="typ">Resource</span><span class="pun">.</span><span class="pln">location</span><span class="pun">)</span></b></code>
                                </li>
                                <li>
                                    <p><b>An Ability to Return Obligations to the Caller</b></p>
                                    <p>Oftentimes, it is insufficient for the application to know whether access is granted or denied, as it needs to know additional information that comes with the decision. For example, the policy may specify a broad <code class="prettyprint"><span class="pln">GRANT</span></code>, but also contain restrictions for some fields, which need to be communicated back to the calling application (aka <code class="prettyprint"><span class="typ">Policy</span><span class="pln"> </span><span class="typ">Enforcement</span><span class="pln"> </span><span class="typ">Point</span></code>, or <code class="prettyprint"><span class="pln">PEP</span></code>). Such return data are called obligations, and provide an important extensibility mechanism for extending application functionality without the need to hardcode security logic. Java security policy does not provide such a capability at all.</p>
                                </li>
                            </ul>
                        </li>
                        <li>
                            <p><b>Governance</b></p>
                            <p>Governance tasks are intended to answer "who can do what, under which conditions" for audit purposes. It is a pretty hard problem to solve in the general case, but Java's permissions-based model makes it close to impossible for any large-scale deployment. Since permissions themselves own the task of interpreting the resources, an external tool needs to take into account all possible permissions used by an arbitrary application in order to understand which application resources are protected and how. Furthermore, such a tool will have to be updated with each revision of the application. Clearly, this presents a maintenance nightmare and does not allow for development of a generic auditing tool for such purposes.</p>
                        </li>
                        <li>
                            <p><a id="DataSec" name="DataSec"><b>Consistency of Data and Function Security</b></a></p>
                            <p>Last, but not least, Java's security model is all about functional security; i.e., it is designed to answer questions about access to particular code functionality. However, in many applications it is equally important to provide a consistent view of the data, redacted according to the user rights. See the section <a href="#PermProliferation">Permissions Proliferation</a> for an example of where and how this functionality could be applied.</p>
                            <p>An alternative, available in Java, usually requires either developing an additional mechanism (for example, via tricks with database queries), or displaying the entire list but then performing access control at the moment when the user tries to access a particular record. The first alternative (having two models) is simply unmanageable and will lead to inconsistencies. The second (performing checks at access time only) reveals too much information, and results in poor usability when a user is shown records, but has no way of accessing them.</p>
                        </li>
                    </ul>
                    <h5 id="Runtime">Runtime Functionality</h5>
                    <p>Runtime experience presents another half of the problem, and Java's model again falls short here for the reasons explained below.</p>
                    <ul>
                        <li>
                            <p><b>Unnecessary Stack Walks</b></p>
                            <p>As has been repeatedly pointed out before, Java's model is intended for code-based security. Correspondingly, upon each request to <a href="http://java.sun.com/javase/6/docs/api/java/lang/SecurityManager.html#checkPermission(java.security.Permission)" target="_blank"><code class="prettyprint"><span class="pln">checkPermission</span><span class="pun">(</span><span class="typ">Permission</span><span class="pun">)</span></code></a>, a <a href="http://java.sun.com/javase/6/docs/technotes/guides/security/spec/security-spec.doc4.html#24646" target="_blank"><code class="prettyprint"><span class="pln">stack walk</span></code></a> is triggered to determine whether all code frames on the stack have the desired permission.</p>
                            <p>However, security policies for large-scale applications care not about the code, but about user checks. In practice, that means that each call to check user privileges turns into an unnecessary stack walk. Moreover, this works at all only as long as the policy processing code (called a <em>Policy Decision Point</em>, or PDP) is co-located in the same process with the application. Due to the hard-coded evaluation logic of <a href="http://java.sun.com/javase/6/docs/api/java/security/AccessController.html" target="_blank"><code class="prettyprint"><span class="typ">AccessController</span></code></a> (see also <a href="#Extensibility">Extensibility of Evaluation Logic</a>), any attempt to externalize the PDP will result in multiple remote calls for each <a href="http://java.sun.com/javase/6/docs/api/java/lang/SecurityManager.html#checkPermission(java.security.Permission)" target="_blank"><code class="prettyprint"><span class="pln">checkPermission</span><span class="pun">(</span><span class="typ">Permission</span><span class="pun">)</span></code></a> invocation, rendering the entire system unusable.</p>
                        </li>
                        <li>
                            <p><a id="Extensibility" name="Extensibility"><b>Extensibility of Evaluation Logic</b></a></p>
                            <p>The logic of Java's security model can be changed only in a single way: by replacing the <a href="http://java.sun.com/javase/6/docs/technotes/guides/security/spec/security-spec.doc3.html#27428" target="_blank"><code class="prettyprint"><span class="pln">security policy provider</span></code></a>. This restriction makes it impossible to customize the stack evaluation algorithm, hard-coded in the <a href="http://java.sun.com/javase/6/docs/api/java/security/AccessController.html" target="_blank"><code class="prettyprint"><span class="typ">AccessController</span></code></a>, to make it more efficient for remote calls, for example, or to optimize it for user-based access control checks.</p>
                        </li>
                        <li>
                            <p><b>Bulk Authorization</b></p>
                            <p>Bulk authorization really helps to improve application performance when multiple elements of a single component need to be authorized, by wrapping multiple requests into a single call to PDP. For example, consider the figure of a web page in the <a href="#Management">Management Functionality</a> section. This page has to make multiple requests to verify all required elements on the page. However, it would be much more efficient to package them together and make just a single call. Unfortunately, once again, Java does not provide any support here.</p>
                            <p>Similar to the bulk authorization is the issue of <em>querying access</em>, which means that the application asks "which resources can a particular user access under a certain resource node, with the given action and attributes?" Depending on the returned result, the application then goes over the child resources, taking specific actions on them. For example, a web page may inquire at load time which controls are accessible to this user, and then hide/disable those that are not permitted by the policy. Java, due to its lack of a single resource model, cannot determine applicable permissions and, therefore, does not have such functionality.</p>
                        </li>
                        <li>
                            <p><b>Return Results</b></p>
                            <p>As has already been explained in <a href="#Elements">Policy Elements</a>, Java lacks the notion of obligations to communicate back to the PEP client additional information about policy decision. This makes it impossible to use JSE-based policies to control data security, for example, as explained in <a href="#DataSec">Consistency of Data and Function Security</a>.</p>
                        </li>
                    </ul>
                    <h3 id="Alternatives">Alternatives</h3>
                    <p>As has been demonstrated earlier in the article, Java SE platform security has never really grown out of its browser-based roots. The administrative and runtime shortcomings associated with using its permission-based model make it a pretty unappealing choice for enterprise-grade applications. Security policy and authorization are areas where Java is clearly lacking. However, this has not hindered its adoption in the enterprise -- instead, most large companies have either developed an in-house solution for authorization, or have purchased third-party software for that purpose. The following subsections, while not attempting to cover the state of the entitlements market, will provide a cursory look at the alternative approaches available to Java architects.</p>
                    <h5 id="XACML">XACML Standard</h5>
                    <p>Standard industry terminology has been incorporated into the <a href="http://docs.oasis-open.org/xacml/access_control-xacml-2_0-core-spec-cd-04.pdf" target="_blank">eXtensible Access Control Markup Language (XACML) standard</a>. Although by itself the standard does not provide protection, its concept serve as a basis for multiple products in the <em>Fine-Grained Entitlements</em> (FGE) industry, helping to define common terminology and components across the board. Many of the concepts, explained in the sections above, are also reflected in the XACML standard.</p>
                    <p>By itself, XACML does not define Java binding -- there is only a SOAP profile that allows sending runtime authorization requests. There was an early attempt by XACML enthusiasts from Sun to define a <a href="http://research.sun.com/projects/xacml/J2SEPolicyProvider.html" target="_blank">J2SE Policy Profile</a> (i.e., a Java Policy Provider for XACML policies), but, judging that it has gone nowhere in the last five years, I do not think it merits serious consideration.</p>
                    <p>Instead, different vendors have been developing products around the XACML standard, making its capabilities available to Java applications via their own APIs, which usually present a simplified (and more manageable!) model than the pure standard. Over time, as it has happened in other areas, we would expect to see a convergence of different types of Java authorization APIs currently available in the market into an industry-standard Java API.</p>
                    <h5 id="JSR115">JSR 115</h5>
                    <p>The <a href="http://java.sun.com/j2ee/javaacc/" target="_blank"><code class="prettyprint"><span class="pln">JSR </span><span class="lit">115</span></code></a> standard has a long and troubled history, since the vendors, although endorsing it formally in 2003, did not really rush to replace their platform-specific solutions with the one prescribed in the JSR. And the big customers did not hurry to embrace it, either -- in fact, since a JACC provider was added to WebLogic shortly after acceptance of the standard, there has not been a single request or inquiry about this feature!</p>
                    <p>The reason for this indifference lies in the intended purpose of the standard itself, and the tools that it offers to the public. JSR 115 was specifically devised to plug a hole in EE-to-SE binding in the authorization area, where the EE and SE mechanisms just co-existed, but did not interact with each other. While having addressed the original need of bringing EE-specific permissions into the SE domain, it still relies on the SE permission model, with all of its shortcomings, discussed above. Besides, it does nothing to address the requirements for fine-grained entitlements, listed in the sections above -- which is what the companies are actually looking for. Although some people argue that its mechanisms (such as <code class="prettyprint"><span class="typ">ContextHandlers</span></code>) are flexible enough to support FGE requirements, the technical and functional merits of such a solution remain highly questionable. These (and other) problems of SE/EE security integration were also covered in my earlier article on this subject, <a href="http://today.java.net/pub/a/today/2006/09/14/using-jaas-in-ee-and-soa.html" target="_blank">"Using JAAS in Java EE and SOA Environments."</a></p>
                    <h3 id="Conclusions">Conclusions</h3>
                    <p>As has been shown above in this article, Java's own permission model is highly inadequate for addressing large-scale enterprise system. This article was not intended to be a full-blown guide on the subject, but rather an introductory overview of the involved issues. For more information on the topic of fine-grained entitlements, it would be advisable to become familiar with the concepts of the XACML standard, as it defines the components and terminology acceptable to all parties. Although open source XACML implementations and toolkits are available today, it would not be advisable to dive in to use them right away, as they just implement the standard, leaving out the many important considerations listed above. Instead, the author would highly recommend considering using one of the third-party fine-grained entitlements solutions available today in the market.</p>
                  
                        </td></tr>
                    <tr><td colspan="2" align="center">
                        <hr />
                        </td>
                    </tr>
                   
                </table>
                </div>
         </div>
    </form>
</body>
</html>
