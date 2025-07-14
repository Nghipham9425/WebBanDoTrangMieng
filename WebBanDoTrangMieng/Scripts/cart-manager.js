// Cart Management - 2-Step Confirmation
$(document).ready(function () {
  // Create confirmation modal if not exists
  if (!$("#removeConfirmModal").length) {
    $("body").append(`
            <div class="modal fade" id="removeConfirmModal" tabindex="-1" style="z-index: 9999;">
                <div class="modal-dialog modal-sm">
                    <div class="modal-content">
                        <div class="modal-header">
                            <h6 class="modal-title">Xác nhận xóa</h6>
                            <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                        </div>
                        <div class="modal-body text-center">
                            <i class="fas fa-exclamation-triangle text-warning mb-2" style="font-size: 2rem;"></i>
                            <p>Bạn có chắc muốn xóa sản phẩm này khỏi giỏ hàng?</p>
                        </div>
                        <div class="modal-footer justify-content-center">
                            <button type="button" class="btn btn-secondary btn-sm" data-bs-dismiss="modal">Hủy</button>
                            <button type="button" class="btn btn-danger btn-sm" id="confirmRemove">Xác nhận xóa</button>
                        </div>
                    </div>
                </div>
            </div>
        `);
  }

  let currentProductId = null;
  let currentButton = null;

  // Remove item - Step 1: Show modal
  $(document)
    .off("click", ".remove-btn")
    .on("click", ".remove-btn", function (e) {
      e.preventDefault();
      e.stopPropagation();

      currentProductId = $(this).data("product-id");
      currentButton = $(this);

      // Use Bootstrap 5 modal API
      const modal = new bootstrap.Modal(
        document.getElementById("removeConfirmModal")
      );
      modal.show();
    });

  // Remove item - Step 2: Confirm in modal
  $(document)
    .off("click", "#confirmRemove")
    .on("click", "#confirmRemove", function () {
      if (currentProductId && currentButton) {
        $(this).prop("disabled", true).text("Đang xóa...");
        currentButton.prop("disabled", true);

        $.post("/Cart/RemoveItem", { productId: currentProductId })
          .done(() => {
            const modal = bootstrap.Modal.getInstance(
              document.getElementById("removeConfirmModal")
            );
            modal.hide();
            location.reload();
          })
          .fail(() => {
            alert("Có lỗi xảy ra, vui lòng thử lại!");
            $(this).prop("disabled", false).text("Xác nhận xóa");
            currentButton.prop("disabled", false);
          });
      }
    });

  // Reset when modal closes (Bootstrap 5 event)
  document
    .getElementById("removeConfirmModal")
    .addEventListener("hidden.bs.modal", function () {
      currentProductId = null;
      currentButton = null;
      $("#confirmRemove").prop("disabled", false).text("Xác nhận xóa");
    });

  // Cart count handled by layout.js globally
});
