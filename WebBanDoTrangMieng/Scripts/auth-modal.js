(function () {
  // Hiện modal khi click user icon
  $(document).on("click", ".user-toggle", function (e) {
    e.preventDefault();
    $("#authModal").modal("show");
  });

  // Gửi form login/register/forgot
  $(document).on(
    "submit",
    "#loginForm, #registerForm, #resetForm",
    function (e) {
      e.preventDefault();
      var $form = $(this);
      var url = $form.attr("action");
      var data = $form.serialize();
      var $btn = $form.find('button[type="submit"]');
      var originalText = $btn.html();

      $btn
        .prop("disabled", true)
        .html(
          '<span class="spinner-border spinner-border-sm me-2"></span>Đang xử lý...'
        );

      $.post(url, data)
        .done(function (res) {
          showAlert(res.message, res.success ? "success" : "danger");
          if (res.success) {
            setTimeout(function () {
              $("#authModal").modal("hide");
              location.reload();
            }, 1200);
          }
        })
        .fail(function () {
          showAlert("Có lỗi xảy ra, vui lòng thử lại!", "danger");
        })
        .always(function () {
          $btn.prop("disabled", false).html(originalText);
        });
    }
  );

  // Chuyển sang tab "Quên mật khẩu?"
  $(document).on("click", "#forgotPasswordLink", function (e) {
    e.preventDefault();
    // Ẩn tất cả tab-pane
    $("#authModal .tab-pane").removeClass("show active");
    // Hiện tab reset
    $("#resetTab").addClass("show active");
    // Ẩn nav-tabs
    $("#authModal .auth-tabs").hide();
  });

  // Quay lại tab đăng nhập từ reset password
  $(document).on("click", "#backToLogin", function (e) {
    e.preventDefault();
    // Ẩn tất cả tab-pane
    $("#authModal .tab-pane").removeClass("show active");
    // Hiện tab login
    $("#loginTab").addClass("show active");
    // Hiện lại nav-tabs
    $("#authModal .auth-tabs").show();
    // Reset active nav-link
    $("#authModal .nav-link").removeClass("active");
    $("#authModal .nav-link[data-bs-target='#loginTab']").addClass("active");
  });

  // Hiển thị alert
  function showAlert(message, type) {
    var html = `<div class="alert alert-${type} alert-dismissible fade show" role="alert">
          ${message}
          <button type="button" class="btn-close" data-bs-dismiss="alert"></button>
      </div>`;
    $("#authModal .tab-pane.active").prepend(html);
    setTimeout(function () {
      $(".alert").fadeOut();
    }, 4000);
  }

  // Reset form khi đóng modal
  $("#authModal").on("hidden.bs.modal", function () {
    $(this)
      .find("form")
      .each(function () {
        this.reset();
      });
    $(this).find(".alert").remove();
    // Reset về tab login
    $("#authModal .tab-pane").removeClass("show active");
    $("#loginTab").addClass("show active");
    $("#authModal .auth-tabs").show();
    $("#authModal .nav-link").removeClass("active");
    $("#authModal .nav-link[data-bs-target='#loginTab']").addClass("active");
  });

  // Toggle hiện/ẩn mật khẩu (nếu muốn giữ)
  $(document).on("click", ".password-toggle", function () {
    var $input = $(this).siblings('input[type="password"], input[type="text"]');
    var $icon = $(this).find("i");
    if ($input.attr("type") === "password") {
      $input.attr("type", "text");
      $icon.removeClass("fa-eye").addClass("fa-eye-slash");
    } else {
      $input.attr("type", "password");
      $icon.removeClass("fa-eye-slash").addClass("fa-eye");
    }
  });
})();
