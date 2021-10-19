/*
 * SimpleModal Confirm Modal Dialog
 * http://www.ericmmartin.com/projects/simplemodal/
 * http://code.google.com/p/simplemodal/
 *
 * Copyright (c) 2010 Eric Martin - http://ericmmartin.com
 *
 * Licensed under the MIT license:
 *   http://www.opensource.org/licenses/mit-license.php
 *
 * Revision: $Id: confirm.js 254 2010-07-23 05:14:44Z emartin24 $
 */
function confirm(message, callback) {
	$('#confirm').modal({
		closeHTML: "<a href='#' title='Close' class='modal-close'>x</a>",
		position: ["20%",],
		overlayId: 'confirm-overlay',
		containerId: 'confirm-container', 
		onShow: function (dialog) {
			var modal = this;

			$('.message', dialog.data[0]).append(message);

			// if the user clicks "yes"
			$('.yes', dialog.data[0]).click(function () {
				// call the callback
				if ($.isFunction(callback)) {
					callback.apply();
				}
				// close the dialog
				modal.close(); // or $.modal.close();
				
			});
			
			// if the user clicks "no" un-comment and use it - Nabin
			$('.no', dialog.data[1]).click(function () {
				
				// close the dialog
				if(document.getElementById('hdnKS'))
				document.getElementById('hdnKS').value = "";
				modal.close(); // or $.modal.close();
			});
		}
	});
}
 function SMARTConfirm(message, callbackYes,callbackNo) 
    {
	    $('#confirm').modal({
		    closeHTML: "<a href='#' title='Close' class='modal-close'>x</a>",
		    position: ["20%",],
		    overlayId: 'confirm-overlay',
		    containerId: 'confirm-container', 
		    onShow: function (dialog) {
			    var modal = this;

			    $('.message', dialog.data[0]).append(message);

			    // if the user clicks "yes"
			    $('.yes', dialog.data[0]).click(function () {
				    // call the callback
				    if ($.isFunction(callbackYes)) {
					    callbackYes.apply();
				    }
				    // close the dialog
				    modal.close(); // or $.modal.close();
    				
			    });
    			
			    // if the user clicks "no" un-comment and use it - Nabin
			    $('.no', dialog.data[1]).click(function () {
    				
				     if ($.isFunction(callbackNo)) {
					    callbackNo.apply();
				    }
				    // close the dialog
				    modal.close(); // or $.modal.close();
			    });
		    }
	    });
    }