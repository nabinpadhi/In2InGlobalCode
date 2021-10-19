$(document).ready(function(){
	$('.viewType li').click(function(){

		if($(this).hasClass('selected')) 
		{
			return false;
		}
	
		$('.viewType li').removeClass('selected');
		$(this).addClass('selected');	
		$('.productContainer').toggleClass('grid');
		return false;
	});
	
	$('.purchaseBtn, a.demoBtn').on('click', function(event) {
	
		var el = $(this);
		var track = true;
		var href = (typeof(el.attr('href')) != 'undefined' ) ? el.attr('href') :"";
		var isThisDomain = href.match(document.domain.split('.').reverse()[1] + '.' + document.domain.split('.').reverse()[0]);
		
		if (!href.match(/^javascript:/i)) 
		{
			var elEv = {}; elEv.value=0, elEv.non_i=false;
			var isInput = false;
			
			if (href.match(/^mailto\:/i)) 
			{
				elEv.category = "EmailTo";
				elEv.action = "click";
				elEv.label = href.replace(/^mailto\:/i, '');
				elEv.loc = href;
			}
			else if (href.match(/^https?\:/i) && !isThisDomain)
			{
				
				elEv.category = "Products";
				elEv.action =  (el.hasClass("demoBtn")) ? "Demo button clicked" : "Purchase button clicked";
				elEv.label = el.data("product-name");
				elEv.non_i = true;
				elEv.loc = href;
			}
			else if (this.tagName.toLowerCase() == "input")
			{
				isInput = true;
				elEv.category = "Products";
				elEv.action =  "Purchase button clicked";
				elEv.label = el.data("product-name");
			}
			else
			{
				track = false;
			}					
	 
			if (track) 
			{
				_gaq.push(['_trackEvent', elEv.category, elEv.action, elEv.label]);

				if ( !isInput && ((!event.metaKey || !event.ctrlKey) && el.attr('target') == undefined) || (el.attr('target') && el.attr('target').toLowerCase() != '_blank')) 
				{
					setTimeout(function() { location.href = elEv.loc; }, 70);
					return false;
				}
			}

			$.ajax(
			{
				url: base_url + 'apay/socinfo',
				type: 'POST',
				data: 
				{
					info: 'gm_info',
					data: JSON.stringify(elEv)
				},
				success: function (data)
				{
					//alert(data);
				}
			});
		}
	});
	
	$('.subscribe .submitBtn').click(function()
	{
		_gaq.push(['_trackEvent', 'Newsletter', 'Subscribe', 'Footer']);
	});
	
	$(function ()
	{
        
		if (!$.isEmptyObject($('#footerKSS')[0]))
		{
			
            $.fn.speedoNotify({
				height: 160,
				theme: 'agapa',
				position: 'bottom',
				effectIn: 'fade',
				effectOut: 'fade',
				css3Effects: 'auto',
				htmlContent: '<img src="./images/in2ingloballogo.png" />',
				//href: base_url + 'files/products/banners/kss-speedo-popup-banner.png',
				//cover: false
			});
		}
	});
});