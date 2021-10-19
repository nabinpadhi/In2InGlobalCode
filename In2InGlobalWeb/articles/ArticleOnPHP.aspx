<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ArticleOnPHP.aspx.cs" Inherits="InGlobal.presentation.articles.ArticleOnPHP" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .blogTitle {
            
            font-weight: bold;
            color: #471654;
            text-align:center;
        }

        .about-scott {
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
            padding-left: 125px;
            background: transparent url('image/photo-scott-tall.jpg') no-repeat left top;
        }

        hr {
            border-color: #e2842c;
            color: #e2842c;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">

        <div style="background-color: #f6f6ea; border-radius: 25px; width: 100%;">
            <div style="width: 98%; height: 530px; overflow-y: auto">
                <table style="width: 98%" cellspacing="10px">
                    <tr><td class="blogTitle"><h1>Top 10+ PHP Best Practices</h1></td></tr>
                    <tr>
                        <td colspan="2">
                            <div class="post-content clearfix" itemprop="articleBody">
                                <a class="project-node-main-image article-image" href="/sites/default/files/article/main_image/secure-coding-php.jpg" itemprop="image" title="Top 10+ PHP Best Practices">
                                    <img src="http://www.ansoncheunghk.info/sites/default/files/imagecache/article_thumb/article/main_image/secure-coding-php.jpg" alt="Top 10+ PHP Best Practices" title="Top 10+ PHP Best Practices" class="imagecache imagecache-article_thumb" itemprop="image"></a>
                                <p>PHP is the most widely-used language for programming on the web. Here are fourteen best practices that every programmer should know and code according to this guidelines.</p>
                                <h5>1. Turn on Error Reporting for development</h5>
                                <p>Error reporting is a very handy function in PHP. By enable it, you might spotted the problem earlier in your code. There are several different level of error reporting; however, by enabling E_ALL can notice most errors, critical and warnings.<br>
                                    <br>
                                    <span style="background-color: red; color: #FFFFFF;"><strong>A very important notice, once your code is ready for production, you should turn off error reporting. Otherwise, the potential problems would be visible to all visitors.</strong></span></p>
                                <h5>2. Apply DRY Approach</h5>
                                <p>DRY stands for Don't Repeat Yourself. It is a very valuable programming concept. And it applied to any language, like C#,PHP,JAVA...etc. DRY approach is to ensure that you do not write redundant code. Let take an example here.</p>
                                <div class="codeblock">$mysql = mysql_connect('127.0.0.1', 'admin', 'admin_password');<br>
                                    mysql_select_db('drupal') or die("cannot select DB");</div>
                                <p>The code is not align with DRY approach.</p>
                                <div class="codeblock">$db_host = '127.0.0.1';<br>
                                    $db_user = 'admin';<br>
                                    $db_password = 'admin_password';<br>
                                    $db_database = 'drupal';<p>$mysql = mysql_connect($db_host, $db_user, $db_password);<br>
                                        mysql_select_db($db_database);</p>
                                </div>
                                <p>You can read more about DRY at <a href="http://en.wikipedia.org/wiki/Don%27t_repeat_yourself" target="_blank">here</a></p>
                                <h5>3. Indent Code and Use White Space for Readability</h5>
                                <p>You should ensure that your code is readable and easy to search by indentations and white space in your code. It is because you’ll most definitely be making changes in the future.</p>
                                <h5>4. Highly recommend to use &lt;?php ?&gt;<!--?php ?--></h5>
                                <p>A lot of programmer would use the shortcuts when declaring PHP. Here are the example.</p>
                                <div class="codeblock">&lt;?<br>
                                    &nbsp;&nbsp;&nbsp; echo "Hello world";<br>
                                    ?&gt;<p>&lt;?="Hello world"; ?&gt;</p>
                                    &lt;% echo "Hello world"; %&gt;</div>
                                <p>To ensure further version support guarantee, it is highly recommended to stick with standard &lt;?php ?&gt;.</p>
                                <h5>5. Always use Meaningful, Consistent Name Standard</h5>
                                <p>There are two popular naming standard:<br>
                                    1. <strong>camelCase:</strong> First letter of each word is capitalized, expect for the first word.<br>
                                    2.&nbsp;<strong>underscores:</strong> Add underscore between words, like mysql_real_escape_string().<br>
                                    You should choice either naming conventions to do you coding.However, be consistent on your coding.</p>
                                <div class="codeblock">class Foo {<p>&nbsp; &nbsp; public function someDummyMethod() {</p>
                                    &nbsp; &nbsp; }<p>}</p>
                                    function my_procedural_function_name() {<p>}<br>
                                        &nbsp;</p>
                                </div>
                                <h5>6. Prevent Deep Nesting</h5>
                                <p>Too many level of nesting would make code difficult to read.</p>
                                <div class="codeblock">function writeFileFunction() {<p>// ...</p>
                                    if (is_writable($folder)) {<p>&nbsp; &nbsp; if ($fp = fopen($file_path,'w')) {</p>
                                    &nbsp; &nbsp; &nbsp; &nbsp;if ($stuff = extractSomeStuff()) {<p>&nbsp; &nbsp; &nbsp; &nbsp; &nbsp;if (fwrite($fp,$stuff)) {</p>
                                    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;// ...<p>&nbsp; &nbsp; &nbsp; &nbsp; &nbsp;} else {<br>
                                        &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;return false;<br>
                                        &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;}<br>
                                        &nbsp; &nbsp; &nbsp; } else {<br>
                                        &nbsp; &nbsp; &nbsp; &nbsp;return false;<br>
                                        &nbsp; &nbsp; &nbsp;}<br>
                                        &nbsp; &nbsp; } else {<br>
                                        &nbsp; &nbsp; &nbsp;return false;<br>
                                        &nbsp; &nbsp; }<br>
                                        &nbsp; } else {<br>
                                        &nbsp; &nbsp;return false;<br>
                                        &nbsp;}<br>
                                        }<br>
                                        &nbsp;</p>
                                </div>
                                <p>Obviously, the code on the above is difficult to read and understand. To enhance the readability,it is always possible to reduce the level of nesting as follow:</p>
                                <div class="codeblock">function writeFileFunction() {<p>// ...</p>
                                    &nbsp; if (!is_writable($folder)) {<br>
                                    &nbsp; &nbsp; return false;<br>
                                    &nbsp; }<p>&nbsp; if (!$fp = fopen($file_path,'w')) {<br>
                                        &nbsp; &nbsp; return false;<br>
                                        &nbsp; }</p>
                                    &nbsp; if (!$stuff = extractSomeStuff()) {<br>
                                    &nbsp; &nbsp; return false;<br>
                                    &nbsp; }<p>&nbsp; if (fwrite($fp,$stuff)) {<br>
                                        &nbsp; &nbsp;// ...<br>
                                        &nbsp; } else {<br>
                                        &nbsp; &nbsp; return false;<br>
                                        &nbsp; }<br>
                                        }</p>
                                </div>
                                <h5>7. Remember to Comment, Comment &amp; Comment</h5>
                                <p>Please ensure that you leave comment inside your source code. It is very important when the project involve five to ten programmer. The comment is very important. It helps out a lot when your team have to go back and maintain a project from a long time ago.<br>
                                    <br>
                                    In order to maintain a high quality of comment standard, it is highly recommened to familiarize yourself with some PHP Documentation packages like <a href="http://manual.phpdoc.org/HTMLframesConverter/default/" target="_blank">phpDocumentor</a>, and take the extra time to do it. It's worth it.</p>
                                <h5>8. Do not put phpinfo() in your web root.</h5>
                                <p>Phpinfo is a very useful function.By simple creating a PHP file that has</p>
                                <div class="codeblock">&lt;?php phpinfo(); ?&gt;</div>
                                <p>and place it to the server somewhere, you can know everything about your server environment. However, a lot of programmer would place the file contain phpinfo() in the webroot. This is a very insecure practice. It could potentially speel doom for you server. Please make sure phpinfo() is in a secure sport and delete it once you are done.</p>
                                <h5>9, Never trust your user</h5>
                                <p>If your application has places for user input, you should always assume that they’re going to try to input naughty code. (We’re not implying that your users are bad people. It’s just a good mindset.) A great way to keep your site hacker-free is to always initialize your variables to safeguard your site from <a href="http://ha.ckers.org/xss.html" target="_blank">XSS attacks</a>. PHP.net has an example of a <a href="http://talks.php.net/show/php-best-practices/19" target="_blank">properly secured form</a> with initialized variables:</p>
                                <div class="codeblock">&nbsp;&lt;?php<br>
                                    if (correct_user($_POST['user'], $_POST['password']) {<br>
                                    &nbsp;&nbsp;&nbsp;&nbsp; $login = true;<br>
                                    }<p>if ($login) {<br>
                                        &nbsp;&nbsp;&nbsp;&nbsp; forward_to_secure_environment();<br>
                                        }<br>
                                        ?&gt;<br>
                                        &nbsp;</p>
                                </div>
                                <h5>10. Use a cache mechanism</h5>
                                <p>There are several robust caching system which are available for free. Have a look of following:</p>
                                <ul>
                                    <li><a href="http://www.danga.com/memcached/" target="_blank">Memcached</a></li>
                                    <li><a href="http://us.php.net/manual/en/intro.apc.php" target="_blank">APC</a></li>
                                    <li><a href="http://xcache.lighttpd.net/" target="_blank">XCache</a></li>
                                    <li><a href="http://files.zend.com/help/Zend-Platform/zend_cache_api.htm" target="_blank">Zend Cache</a></li>
                                    <li><a href="http://www.eaccelerator.net/" target="_blank">eAccelerator</a></li>
                                </ul>
                                <p>&nbsp;</p>
                                <h5>11. Keep Functions Outside of Loops</h5>
                                <p>You will suffer a hit of performance when you include functions inside of loops. The larger the loop you have, the longer the execution time will take. Take some time, to place the function outside the loop.</p>
                                <p>Bad example:</p>
                                <div class="codeblock">for ($i = 0; $i &lt; count($array); $i++) {<br>
                                    &nbsp; //stuff<br>
                                    }</div>
                                <p>Good example:</p>
                                <div class="codeblock">$count = count($array);<br>
                                    for($i = 0; $i &lt; $count; $i++) {<br>
                                    &nbsp; //stuff<br>
                                    }</div>
                                <h5>12. Do not copy extra variables</h5>
                                <p>Some people like to make their code more appealing by copying predefined variables to smaller-named variables. However, this is bad coding practices and hurt the performance and potentially double the memory usage of your script. Here is an example give by Google Code to illustrate good and bad examples of variable usage:</p>
                                <p>Bad example:</p>
                                <div class="codeblock">$description = strip_tags($_POST['description']);<br>
                                    echo $description;</div>
                                <p>Good example:</p>
                                <div class="codeblock">echo strip_tags($_POST['description']);</div>
                                <h5>13. Protect your Script From SQL Injection</h5>
                                <p>If you don’t escape your characters used in SQL strings, your code is vulnerable to SQL injections. You can avoid this by using the mysql_real_escape_string.</p>
                                <p>Here’s an example of mysql_real_escape_string in action:</p>
                                <div class="codeblock">$username = mysql_real_escape_string( $GET['username'] );</div>
                                <h5>14. Use framework</h5>
                                <p>Once you have learned about the basics of PHP, you may try out a PHP framework. There are tons of different PHP frameworks. However, many of those are design based on Model-view-contorller (MVC) software architecture. One of the reason that framework is being popular. It is because MVC architecture ensure clear spearation between data, logic and html which ensure ease of maintaince and developement. Also, it ensure different programmer to read and understand the codes easily.<br>
                                    <img alt="MVC" src="http://upload.wikimedia.org/wikipedia/commons/b/b5/ModelViewControllerDiagram2.svg" style="width: 312px; height: 142px; padding-top: 3px; padding-right: 3px; padding-bottom: 3px; padding-left: 3px; margin-top: 4px; margin-right: 10px; margin-bottom: 4px; margin-left: 10px; border-top-color: rgb(183, 169, 154); border-right-color: rgb(183, 169, 154); border-bottom-color: rgb(183, 169, 154); border-left-color: rgb(183, 169, 154); border-top-left-radius: 4px 4px; border-top-right-radius: 4px 4px; border-bottom-right-radius: 4px 4px; border-bottom-left-radius: 4px 4px; border-top-width: 1px; border-right-width: 1px; border-bottom-width: 1px; border-left-width: 1px; border-top-style: solid; border-right-style: solid; border-bottom-style: solid; border-left-style: solid;"><br>
                                    Following are the popular PHP Frameworks, Template Engines &amp; PHP based Content Management System<br>
                                    <br>
                                    PHP Frameworks</p>
                                <ul>
                                    <li><a href="http://framework.zend.com/" target="_blank">Zend Framework</a></li>
                                    <li><a href="http://codeigniter.com/" target="_blank">CodeIgniter</a></li>
                                    <li><a href="http://cakephp.org/" target="_blank">CakePHP</a></li>
                                    <li><a href="http://www.symfony-project.org/" target="_blank">Symfony</a></li>
                                </ul>
                                <p>Popular Template Engines:</p>
                                <ul>
                                    <li><a href="http://www.smarty.net/" target="_blank">Smarty</a></li>
                                    <li><a href="http://dwoo.org/" target="_blank">Dwoo</a></li>
                                </ul>
                                <p>Popular Content Management Systems</p>
                                <ul>
                                    <li><a href="http://drupal.org/" target="_blank">Drupal</a></li>
                                    <li><a href="http://ez.no" target="_blank">EzPublish</a></li>
                                    <li><a href="http://www.joomla.org" target="_blank">Joomla</a></li>
                                    <li><a href="http://wordpress.org" target="_blank">WordPress</a></li>
                                </ul>                                                               
                            </div>
                        </td>
                    </tr>
                </table>
            </div>
        </div>
    </form>
</body>
</html>
