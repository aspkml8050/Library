var apiTable;

function openMainLib() {
	$('#ApiSpinner').addClass('loader');

    $('#dvMainLibraryApi').draggable();
    $('#dvMainLibraryApi').removeClass('hideApi');
	$('#dvhtml').load('BookCatalogFromApi.html');
	$('#ApiSpinner').removeClass('loader');
	$('#tabApiData').DataTable();
	apiTable= $('#tabApiData').DataTable();
    return false;
}
function closit() {
	$('#dvMainLibraryApi').addClass('hideApi');
}

function copyToClipboard(copyText) {
	alert(copyText);
	copyText.select();
	copyText.setSelectionRange(0, 99999); // For mobile devices

	// Copy the text inside the text field
	navigator.clipboard.writeText(copyText.value);
}

var urlBase;
function ApiCall(url) {
	urlBase = url;
	// The custom URL entered by user
	var URL = url+ '/Book/GetModule';
	var settings = {

		// Defines the configurations
		// for the request  jsonp
		cache: false,
		dataType: "json",
		async: true,
		crossDomain: true,
		url: URL,
		method: "GET",
		headers: {
			accept: "application/json",
			"Access-Control-Allow-Origin": "*",
		},

		// Defines the response to be made
		// for certain status codes
		statusCode: {
			200: function (response) {
				console.log("Status 200: Page is up!");
				$('#dverror').removeClass('has-error');
			},
			400: function (response) {
				console.log("Status 400: Page is down.");
				$('#dverror').text('Connection Error from Api');
				//style="color:red; font-weight:bold"
			},
			405: function (response) {
				console.log("Status 400: Page is down.");
				$('#dverror').text('Connection Error from Api');
			},
			0: function (response) {
				console.log("Status 0: Page is down.");
				$('#dverror').text('Connection Error from Api');
			},
		},
	};

	// Sends the request and observes the response
	$.ajax(settings).done(function (response) {
		ApiSetupMods(response);
	});
}

function ApiSetupMods(resp) {
//	console.log(resp);
	if (resp.isSuccess == true) {
		let data = resp.data;
		let dvs = $('#dvModules');
		dvs.empty();
		for (let ixd = 0; ixd < data.length; ixd++) {
			let chk1 = '<input type="checkbox"  name="modid'+ ixd + '" id="modid' + ixd + '" value="' + data[ixd].typeId + '" />';
			let lab = '<label style="padding-right:10px;"  for="modid' + ixd + '">' + data[ixd].typeName + '</label>';
			dvs.append(chk1);
			dvs.append(lab);
		}
	}
}

function getApiData() {
	//tabApiData

	let txt = $('#inpApiSearch').val();
	txt = txt.trim();
	if (txt == '') {
		alert('... Some text Required');
		$('#inpApiSearch').focus();
		return;
	}
	$('#ApiSpinner').addClass('loader');
	let cks = $('input[id*=modid]');
	let opts = [];
	let roid = 1;
	for (let ixd = 0; ixd < cks.length; ixd++) {
		let check = $(cks[ixd]).is(':checked');
		let val = $(cks[ixd]).val();
		if (check) {
			let opt = {
				'rowId': roid,
				'rowValue': val
			};
			opts.push(opt);
			roid++;
		};
	}
	let comid = {
		'commonId': '-1'
	};
	let srs = {
		'searchText':   txt 
	};
	let modls = {
		'modTypeList': opts
	};
	let usr = {
		'userId': '0'
	};
	let frm = {
		'formId': 0
	};
	let typ = {
		'type': 1
	};
	let dsend = {
		'commonId': '-1',
		'searchText': txt,
		'modTypeList': opts,
		'userId': '0',
		'formId': 0,
		'type': 1

	};
	let dsendStr = JSON.stringify(dsend);
	
	let url = urlBase + '/Book/GetCatalog';
	var URL = url;
	$.ajax({
		cache: false,
		url: URL,
		type: "POST",
		datatype: "json",
		headers: {
			'Accept': 'application/json',
			'Content-Type': 'application/json'
		},
		data: dsendStr,
		success: function (html) {
			$('#ApiSpinner').removeClass('loader');
			let dvcont = $('#dvApiData');
			let suc = html.isSuccess;
			let d = html.data;
			if (suc == true) {

				if (d.length > 0) {
					apiTable = $('#tabApiData').DataTable();
					apiTable.destroy();
					$('#tabApiData >tbody >tr').remove();
					let tr = '';
					for (let xd = 0; xd < d.length; xd++) {
						tr += '<tr>';
						tr += '<td>' + d[xd].typeName + '</td>';
						tr += '<td>' + d[xd].title + '</td>';
						tr += '<td>' + d[xd].authorName + '</td>';
						tr += '<td>' + d[xd].subjectName + '</td>';
						tr += '<td>' + d[xd].languageName + '</td>';
						tr += '</tr>';
					}
					$('#tabApiData > tbody ').append(tr);
					$('#tabApiData > tbody >tr >td ').on('click', function () {
						//copyToClipboard($(this).text());
						var temp = $("<input>");
						$("body").append(temp);
						temp.val($(this).text()).select();
						document.execCommand("copy");
						temp.remove();
						$(this).css('color', 'green');
					});
					$('#tabApiData').addClass('display compact');
					apiTable = $('#tabApiData').DataTable();
					$('#tabApiData').DataTable();
					
					
				}
			
			}
		},
		error: function (data) {
			console.log(data.status + ':' + data.statusText, data.responseText);
			$('#ApiSpinner').removeClass('loader');
		}
	});


	return false;
}