// Custom Alert System - Compact Version
function showCustomAlert(message, type = "info", title = "Thông báo") {
  const existingAlert = document.getElementById("customAlertModal");
  if (existingAlert) existingAlert.remove();

  const colors = {
    success: "success",
    error: "danger",
    danger: "danger",
    warning: "warning",
    info: "info",
  };
  const icons = {
    success: "fa-check-circle",
    error: "fa-exclamation-triangle",
    danger: "fa-exclamation-triangle",
    warning: "fa-exclamation-circle",
    info: "fa-info-circle",
  };

  const color = colors[type] || "primary";
  const icon = icons[type] || "fa-bell";

  const modalHTML = `
        <div class="modal fade" id="customAlertModal" tabindex="-1">
            <div class="modal-dialog modal-dialog-centered">
                <div class="modal-content border-0 shadow">
                    <div class="modal-header bg-${color} text-white">
                        <h5 class="modal-title fw-semibold">
                            <i class="fas ${icon} me-2"></i>${title}
                        </h5>
                        <button type="button" class="btn-close btn-close-white" data-bs-dismiss="modal"></button>
                    </div>
                    <div class="modal-body p-4">
                        <p class="mb-0">${message}</p>
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-${color}" data-bs-dismiss="modal">
                            <i class="fas fa-check me-2"></i>Đồng ý
                        </button>
                    </div>
                </div>
            </div>
        </div>`;

  document.body.insertAdjacentHTML("beforeend", modalHTML);
  const modal = new bootstrap.Modal(
    document.getElementById("customAlertModal")
  );
  modal.show();

  document
    .getElementById("customAlertModal")
    .addEventListener("hidden.bs.modal", function () {
      this.remove();
    });
}

// Custom confirm dialog
function showCustomConfirm(
  message,
  title = "Xác nhận",
  onConfirm = null,
  onCancel = null
) {
  const existingConfirm = document.getElementById("customConfirmModal");
  if (existingConfirm) existingConfirm.remove();

  const modalHTML = `
        <div class="modal fade" id="customConfirmModal" tabindex="-1">
            <div class="modal-dialog modal-dialog-centered">
                <div class="modal-content border-0 shadow">
                    <div class="modal-header bg-warning text-white">
                        <h5 class="modal-title fw-semibold">
                            <i class="fas fa-question-circle me-2"></i>${title}
                        </h5>
                        <button type="button" class="btn-close btn-close-white" data-bs-dismiss="modal"></button>
                    </div>
                    <div class="modal-body p-4">
                        <p class="mb-0">${message}</p>
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-secondary" data-bs-dismiss="modal" id="cancelBtn">
                            <i class="fas fa-times me-2"></i>Hủy
                        </button>
                        <button type="button" class="btn btn-warning" id="confirmBtn">
                            <i class="fas fa-check me-2"></i>Xác nhận
                        </button>
                    </div>
                </div>
            </div>
        </div>`;

  document.body.insertAdjacentHTML("beforeend", modalHTML);
  const modal = new bootstrap.Modal(
    document.getElementById("customConfirmModal")
  );
  modal.show();

  document.getElementById("confirmBtn").addEventListener("click", function () {
    modal.hide();
    if (onConfirm) onConfirm();
  });

  document.getElementById("cancelBtn").addEventListener("click", function () {
    modal.hide();
    if (onCancel) onCancel();
  });

  document
    .getElementById("customConfirmModal")
    .addEventListener("hidden.bs.modal", function () {
      this.remove();
    });
}

// Override default functions
window.originalAlert = window.alert;
window.alert = function (message) {
  showCustomAlert(message, "info", "Thông báo");
};

window.originalConfirm = window.confirm;
window.confirm = function (message) {
  return new Promise((resolve) => {
    showCustomConfirm(
      message,
      "Xác nhận",
      () => resolve(true),
      () => resolve(false)
    );
  });
};
