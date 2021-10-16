<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ModalFailure.ascx.cs" Inherits="WebFilm.Controls.Utilities.Modal.ModalFailure" %>
<svg xmlns="http://www.w3.org/2000/svg" style="display: none;">
    <symbol id="exclamation-triangle-fill" fill="currentColor" viewBox="0 0 16 16">
        <path d="M8.982 1.566a1.13 1.13 0 0 0-1.96 0L.165 13.233c-.457.778.091 1.767.98 1.767h13.713c.889 0 1.438-.99.98-1.767L8.982 1.566zM8 5c.535 0 .954.462.9.995l-.35 3.507a.552.552 0 0 1-1.1 0L7.1 5.995A.905.905 0 0 1 8 5zm.002 6a1 1 0 1 1 0 2 1 1 0 0 1 0-2z" />
    </symbol>
</svg>
<div class="modal fade" id="failureModal" tabindex="-1" aria-labelledby="failure-modal-label"
    aria-hidden="true">
    <div class="modal-dialog modal-fullscreen-md-down">
        <div class="modal-content">
            <div class="modal-header">
                <a href="#" class="btn-close" data-bs-dismiss="modal" aria-label="Close"><i
                    class="fas fa-times"></i></a>
                <h5 class="modal-title">Thông báo</h5>
            </div>
            <div class="modal-body alert alert-danger d-flex align-items-center" role="alert">
                <svg class="bi flex-shrink-0 me-2" width="24" height="24" role="img" aria-label="Danger:">
                    <use xlink:href="#exclamation-triangle-fill" />
                </svg>
                <div>
                    <%=this.Message %>
                </div>
            </div>
        </div>
    </div>
</div>

<script type="text/javascript">
    function showFailureModal() {
        var failureModal = new bootstrap.Modal(document.getElementById('failureModal'), {});
        failureModal.toggle();
    }

    <%if (IsBack)
    {%>
    document.getElementById('failureModal').addEventListener('hidden.bs.modal', function (event) {
        window.history.back(-1);
    })
        <%}
    else
    {%>
    document.getElementById('failureModal').addEventListener('hidden.bs.modal', function (event) {
        window.location.href = window.location.href;
    })<%}%>
</script>
