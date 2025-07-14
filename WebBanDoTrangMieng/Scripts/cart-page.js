// Cart Page Management - Clean & Organized
$(document).ready(function () {
  // Initialize cart page functionality
  CartPage.init();
});

const CartPage = {
  // Initialize all cart page functions
  init: function () {
    this.bindQuantityEvents();
    this.bindPromotionEvents();
    console.log("Cart page initialized successfully");
  },

  // Bind quantity increase/decrease events
  bindQuantityEvents: function () {
    // Increase quantity
    $(".increase-btn").click(function () {
      var productId = $(this).data("product-id");
      var input = $(this).siblings(".quantity-input");
      var newQuantity = parseInt(input.val()) + 1;
      CartPage.updateCartQuantity(productId, newQuantity, input);
    });

    // Decrease quantity
    $(".decrease-btn").click(function () {
      var productId = $(this).data("product-id");
      var input = $(this).siblings(".quantity-input");
      var currentQuantity = parseInt(input.val());
      if (currentQuantity > 1) {
        var newQuantity = currentQuantity - 1;
        CartPage.updateCartQuantity(productId, newQuantity, input);
      }
    });
  },

  // Bind promotion-related events
  bindPromotionEvents: function () {
    // Apply promotion button
    $("#applyPromoBtn").click(function () {
      var promoCode = $("#promoCode").val().trim();
      if (!promoCode) {
        CartPage.showToast(
          '<i class="fas fa-exclamation-triangle"></i> Vui lòng nhập mã giảm giá',
          "error"
        );
        return;
      }
      CartPage.applyPromotion(promoCode);
    });

    // Enter key in promotion input
    $("#promoCode").keypress(function (e) {
      if (e.which == 13) {
        $("#applyPromoBtn").click();
      }
    });
  },

  // Update cart quantity via AJAX
  updateCartQuantity: function (productId, quantity, inputElement) {
    $.ajax({
      url: "/Cart/UpdateCart",
      type: "POST",
      data: {
        productId: productId,
        quantity: quantity,
      },
      success: function (response) {
        if (response.success) {
          inputElement.val(quantity);

          // Update item total price
          var itemRow = inputElement.closest("[data-product-id]");
          var priceText = itemRow.find(".item-price").text();
          var price = parseInt(
            priceText.replace(/\./g, "").replace(/,/g, "").replace("đ", "")
          );
          var itemTotal = price * quantity;
          itemRow.find(".item-total").text(itemTotal.toLocaleString() + "đ");
          itemRow.find(".small.text-muted").text("x " + quantity);

          CartPage.updateCartDisplay(response);
          CartPage.showToast(
            '<i class="fas fa-check-circle"></i> Giỏ hàng đã được cập nhật',
            "success"
          );
        } else {
          CartPage.showToast(
            '<i class="fas fa-exclamation-triangle"></i> ' + response.message,
            "error"
          );
        }
      },
      error: function () {
        CartPage.showToast(
          '<i class="fas fa-exclamation-triangle"></i> Có lỗi xảy ra',
          "error"
        );
      },
    });
  },

  // Update cart display with new totals
  updateCartDisplay: function (response) {
    // Update subtotal
    $(".subtotal-amount").text(response.cartTotal + "đ");

    // Calculate total (subtotal + shipping)
    var subtotalNum = parseInt(
      response.cartTotal.replace(/\./g, "").replace(/,/g, "")
    );
    var shippingFee = 20000;
    var total = subtotalNum + shippingFee;

    // Update display
    $(".cart-subtotal").text(total.toLocaleString() + "đ");
    $(".cart-total").text(total.toLocaleString() + "đ");
  },

  // Apply promotion code
  applyPromotion: function (promoCode) {
    $("#applyPromoBtn").prop("disabled", true).text("Đang xử lý...");

    $.ajax({
      url: "/Cart/ApplyPromotion",
      type: "POST",
      data: { promoCode: promoCode },
      success: function (response) {
        if (response.success) {
          // Show discount info
          $("#discountCode").text(response.promoCode || "N/A");
          $(".discount-amount").text(
            "-" + (response.discountAmount || "0") + "đ"
          );
          $("#discountRow").show();

          // Update total
          $(".cart-total").text(response.newTotal + "đ");

          // Change button to "Remove"
          $("#applyPromoBtn")
            .removeClass("btn-outline-success")
            .addClass("btn-outline-danger")
            .text("Hủy mã")
            .off("click")
            .click(function () {
              CartPage.removePromotion();
            });

          // Disable input
          $("#promoCode").prop("readonly", true);

          // Show success message
          $("#promoResult")
            .html(
              `
                        <div class="alert alert-success py-2 mb-0">
                            <i class="fas fa-check-circle"></i> 
                            ${response.message} (Giảm ${response.discountPercent}%)
                        </div>
                    `
            )
            .show();

          CartPage.showToast(
            '<i class="fas fa-check-circle"></i> ' + response.message,
            "success"
          );
        } else {
          $("#promoResult")
            .html(
              `
                        <div class="alert alert-danger py-2 mb-0">
                            <i class="fas fa-exclamation-triangle"></i> 
                            ${response.message}
                        </div>
                    `
            )
            .show();
          CartPage.showToast(
            '<i class="fas fa-exclamation-triangle"></i> ' + response.message,
            "error"
          );
        }
      },
      error: function () {
        CartPage.showToast(
          '<i class="fas fa-exclamation-triangle"></i> Có lỗi xảy ra',
          "error"
        );
      },
      complete: function () {
        $("#applyPromoBtn").prop("disabled", false);
      },
    });
  },

  // Remove promotion code
  removePromotion: function () {
    $.ajax({
      url: "/Cart/RemovePromotion",
      type: "POST",
      success: function (response) {
        if (response.success) {
          // Hide discount info
          $("#discountRow").hide();

          // Update total
          $(".cart-total").text(response.newTotal + "đ");

          // Reset button
          $("#applyPromoBtn")
            .removeClass("btn-outline-danger")
            .addClass("btn-outline-success")
            .text("Áp dụng")
            .off("click")
            .click(function () {
              var promoCode = $("#promoCode").val().trim();
              if (!promoCode) {
                CartPage.showToast(
                  '<i class="fas fa-exclamation-triangle"></i> Vui lòng nhập mã giảm giá',
                  "error"
                );
                return;
              }
              CartPage.applyPromotion(promoCode);
            });

          // Enable input and clear
          $("#promoCode").prop("readonly", false).val("");
          $("#promoResult").hide();

          CartPage.showToast(
            '<i class="fas fa-check-circle"></i> ' + response.message,
            "success"
          );
        }
      },
      error: function () {
        CartPage.showToast(
          '<i class="fas fa-exclamation-triangle"></i> Có lỗi xảy ra',
          "error"
        );
      },
    });
  },

  // Show toast notification
  showToast: function (message, type = "success") {
    var bgClass = type === "success" ? "bg-success" : "bg-danger";
    var toast = $(`
            <div class="toast align-items-center text-white ${bgClass} border-0" role="alert">
                <div class="d-flex">
                    <div class="toast-body">
                        ${message}
                    </div>
                    <button type="button" class="btn-close btn-close-white me-2 m-auto" data-bs-dismiss="toast"></button>
                </div>
            </div>
        `);

    $(".toast-container").append(toast);
    toast.toast("show");

    setTimeout(function () {
      toast.remove();
    }, 3000);
  },
};
