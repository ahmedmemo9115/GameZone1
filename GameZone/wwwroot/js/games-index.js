$(document).ready(function ()
{
    $('.js-delete').on('click', function () {

        var btn = $(this);

        const swal = Swal.mixin({
            customClass: {
                confirmButton: "btn btn-danger mx-2",
                cancelButton: "btn btn-success"
            },
            buttonsStyling: false
        });

       swal.fire({
            title: "Are you sure that you want to Delete this Game?",
            text: "You won't be able to revert this!",
            icon: "warning",
            showCancelButton: true,
            confirmButtonText: "Yes, delete it!",
            cancelButtonText: "No, cancel!",
            reverseButtons: true
       }).then((result) => {


            if (result.isConfirmed) {
                $.ajax({
                    url: `/Games/Delete/${btn.data('id')}`,
                    method: 'DELETE',
                    success: function () {
                        swal.fire({
                            title: "Deleted!",
                            text: "Game has been deleted.",
                            icon: "success"
                        });
                        btn.parents('tr').fadeOut();
                    },
                    error: function () {
                        swal.fire({
                            title: "Ooooops !",
                            text: "Something Went Wrong.",
                            icon: "error"
                        });
                    }
                })
            } 
        });


        //$.ajax({
        //    url: `/Games/Delete/${btn.data('id')}` ,
        //    method: 'DELETE',
        //    success: function () {
        //        swal('success');
        //},
        //    Error: function () {
        //    alert('Error');
        //}
        //})
    }
    )



}

)