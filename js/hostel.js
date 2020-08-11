


$(function () {
// Get the modal
var modal = document.getElementById('id01');

// When the user clicks anywhere outside of the modal, close it
window.onclick = function(event) {
    if (event.target == modal) {
        modal.style.display = "none";
    }
}
$('#btnLogin').click(function(){
		if($('#user').val() != "" && $('#pwd').val() != "" )
		{
			$('#id01').hide();
			var ID = $('#user').val();
				$('#btnLogout').click(function(){
				location.href= "QuanLy.html";
			$('#navbar-right-nv').html('Chào bạn ' +ID).css({color: "white", fontWeight:"bold",paddingRight: "50px"});
			$('#navbar-right-nv').append('<input type="button" value="log out" id="btnLogout">');
			$('#btnLogout').addClass('btn btn-success').css({margin: "5px"});
	})
		}
	});

})
