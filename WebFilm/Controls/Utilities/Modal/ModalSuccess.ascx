<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ModalSuccess.ascx.cs" Inherits="WebFilm.Controls.Utilities.Modal.ModalSuccess" %>

<svg xmlns="http://www.w3.org/2000/svg" style="display: none;">
    <symbol id="check-circle-fill" fill="currentColor" viewBox="0 0 16 16">
        <path d="M16 8A8 8 0 1 1 0 8a8 8 0 0 1 16 0zm-3.97-3.03a.75.75 0 0 0-1.08.022L7.477 9.417 5.384 7.323a.75.75 0 0 0-1.06 1.06L6.97 11.03a.75.75 0 0 0 1.079-.02l3.992-4.99a.75.75 0 0 0-.01-1.05z" />
    </symbol>
</svg>
<div class="modal fade" id="successModal" tabindex="-1" aria-labelledby="success-modal-label"
    aria-hidden="true">
    <div class="modal-dialog modal-fullscreen-md-down">
        <div class="modal-content">
            <div class="modal-header">
                <a href="#" class="btn-close" data-bs-dismiss="modal" aria-label="Close"><i
                    class="fas fa-times"></i></a>
                <h5 class="modal-title">Thông báo</h5>
            </div>
            <div class="modal-body alert alert-success d-flex align-items-center" role="alert">
                <svg class="bi flex-shrink-0 me-2" width="24" height="24" role="img" aria-label="Success:">
                    <use xlink:href="#check-circle-fill" />
                </svg>
                <div>
                    <%=this.Message %>
                </div>
            </div>
        </div>
    </div>
</div>

<script type="text/javascript">
    function showSuccessModal() {
        var successModal = new bootstrap.Modal(document.getElementById('successModal'), {});
        successModal.toggle();
    }

    <%if (IsBack)
    {%>
    document.getElementById('successModal').addEventListener('hidden.bs.modal', function (event) {
        window.history.back(-1);
    })
        <%}
    else
    {%>
    document.getElementById('successModal').addEventListener('hidden.bs.modal', function (event) {
        window.location.href = window.location.href;
    })<%}%>
</script>