/*
 * ==========================================
 * LAYOUT.JS - Main Layout JavaScript
 * Website: Đồ Tráng Miệng
 * ==========================================
 */

// Khởi tạo khi trang được load
$(document).ready(function () {
  LayoutManager.init();
});

// ==========================================
// LAYOUT MANAGER - Main Controller
// ==========================================
const LayoutManager = {
  // Khởi tạo tất cả components
  init: function () {
    this.initNavigation();
    this.initDropdowns();
    this.initToggleButtons();
    this.initSmoothScroll();
    this.initTooltips();

    console.log("Layout Manager initialized successfully");
  },

  // ==========================================
  // NAVIGATION MANAGEMENT
  // ==========================================
  initNavigation: function () {
    // Mobile menu toggle
    const mobileToggle = $(".navbar-toggler");
    const mobileMenu = $(".navbar-collapse");

    mobileToggle.on("click", function () {
      mobileMenu.toggleClass("show");
      $(this).toggleClass("active");
    });

    // Active menu item highlight
    this.highlightActiveMenu();

    // Scroll effect for navbar
    this.initNavbarScroll();
  },

  // Highlight menu active state
  highlightActiveMenu: function () {
    const currentPath = window.location.pathname;
    $(".nav-link").each(function () {
      const linkPath = $(this).attr("href");
      if (linkPath && currentPath.includes(linkPath)) {
        $(this).addClass("active");
        $(this).closest(".nav-item").addClass("active");
      }
    });
  },

  // Navbar scroll effect
  initNavbarScroll: function () {
    let lastScrollTop = 0;
    const navbar = $(".main-navbar");

    $(window).scroll(function () {
      const scrollTop = $(this).scrollTop();

      if (scrollTop > 100) {
        navbar.addClass("scrolled");
      } else {
        navbar.removeClass("scrolled");
      }

      // Hide/show navbar on scroll
      if (scrollTop > lastScrollTop && scrollTop > 200) {
        navbar.addClass("navbar-hidden");
      } else {
        navbar.removeClass("navbar-hidden");
      }

      lastScrollTop = scrollTop;
    });
  },

  // ==========================================
  // DROPDOWN MANAGEMENT
  // ==========================================
  initDropdowns: function () {
    // Custom dropdown hover
    $(".nav-item.dropdown").hover(
      function () {
        $(this).find(".dropdown-menu").stop(true, true).fadeIn(200);
        $(this).addClass("show");
      },
      function () {
        $(this).find(".dropdown-menu").stop(true, true).fadeOut(200);
        $(this).removeClass("show");
      }
    );

    // Prevent dropdown from closing on click inside
    $(".dropdown-menu").on("click", function (e) {
      e.stopPropagation();
    });

    // Close dropdown when clicking outside
    $(document).on("click", function () {
      $(".dropdown-menu").fadeOut(200);
      $(".nav-item.dropdown").removeClass("show");
    });
  },

  // ==========================================
  // TOGGLE BUTTONS
  // ==========================================
  initToggleButtons: function () {
    // Search toggle
    $(".search-toggle").on("click", function (e) {
      e.preventDefault();
      SearchManager.toggle();
    });

    // Cart toggle
    $(".cart-toggle").on("click", function (e) {
      e.preventDefault();
      CartManager.toggle();
    });

    // User menu toggle - handled by auth-modal.js
  },

  // ==========================================
  // SMOOTH SCROLL
  // ==========================================
  initSmoothScroll: function () {
    $('a[href^="#"]:not([href="#"]):not([href^="#authModal"])').on(
      "click",
      function (e) {
        const href = this.getAttribute("href");
        if (href && href.length > 1) {
          const target = $(href);
          if (target.length) {
            e.preventDefault();
            $("html, body")
              .stop()
              .animate(
                {
                  scrollTop: target.offset().top - 80,
                },
                600,
                "easeInOutExpo"
              );
          }
        }
      }
    );
  },

  // ==========================================
  // TOOLTIPS
  // ==========================================
  initTooltips: function () {
    // Initialize Bootstrap tooltips if available
    if (typeof bootstrap !== "undefined" && bootstrap.Tooltip) {
      const tooltipTriggerList = [].slice.call(
        document.querySelectorAll('[data-bs-toggle="tooltip"]')
      );
      tooltipTriggerList.map(function (tooltipTriggerEl) {
        return new bootstrap.Tooltip(tooltipTriggerEl);
      });
    }
  },
};

// ==========================================
// SEARCH MANAGER
// ==========================================
const SearchManager = {
  isOpen: false,

  toggle: function () {
    if (this.isOpen) {
      this.close();
    } else {
      this.open();
    }
  },

  open: function () {
    // Create search overlay if not exists
    if (!$("#searchOverlay").length) {
      this.createSearchOverlay();
    }

    $("#searchOverlay").fadeIn(300);
    $("#searchInput").focus();
    this.isOpen = true;
    $("body").addClass("search-open");
  },

  close: function () {
    $("#searchOverlay").fadeOut(300);
    this.isOpen = false;
    $("body").removeClass("search-open");
  },

  createSearchOverlay: function () {
    const searchHTML = `
        <div id="searchOverlay" class="search-overlay">
            <div class="search-container">
                <div class="search-header">
                    <h3>Tìm kiếm sản phẩm</h3>
                    <button class="search-close" onclick="SearchManager.close()">×</button>
                </div>
                <form action="/Product" method="GET" class="search-form">
                    <input type="text" id="searchInput" name="search" 
                           placeholder="Nhập tên sản phẩm..." class="search-input">
                    <button type="submit" class="search-btn">Tìm kiếm</button>
                </form>
                <div class="search-suggestions">
                    <h5>Gợi ý:</h5>
                    <div class="suggestion-tags">
                        <span class="suggestion-tag">Bánh flan</span>
                        <span class="suggestion-tag">Tiramisu</span>
                        <span class="suggestion-tag">Bánh cheesecake</span>
                        <span class="suggestion-tag">Pudding</span>
                    </div>
                </div>
            </div>
        </div>
    `;

    $("body").append(searchHTML);

    // Đóng khi click bên ngoài
    $("#searchOverlay").click(function (e) {
      if (e.target === this) {
        SearchManager.close();
      }
    });

    // Đóng khi nhấn ESC
    $(document).keydown(function (e) {
      if (e.key === "Escape" && SearchManager.isOpen) {
        SearchManager.close();
      }
    });

    // Click vào gợi ý
    $(".suggestion-tag").click(function () {
      $("#searchInput").val($(this).text());
      $(".search-form").submit();
    });
  },
};

// ==========================================
// CART MANAGER
// ==========================================
const CartManager = {
  toggle: function () {
    // Redirect to cart page or show cart modal
    window.location.href = "/Cart";
  },

  addToCart: function (productId, quantity = 1) {
    // AJAX call to add product to cart
    $.ajax({
      url: "/Cart/AddToCart",
      method: "POST",
      data: {
        productId: productId,
        quantity: quantity,
      },
      success: function (response) {
        if (response.success) {
          NotificationManager.show("Đã thêm vào giỏ hàng!", "success");
          CartManager.updateCartCount(response.cartCount);
        } else {
          NotificationManager.show("Có lỗi xảy ra!", "error");
        }
      },
      error: function () {
        NotificationManager.show("Không thể kết nối server!", "error");
      },
    });
  },

  updateCartCount: function (count) {
    $(".cart-count").text(count).show();
    if (count > 0) {
      $(".cart-count").addClass("has-items");
    } else {
      $(".cart-count").removeClass("has-items");
    }
  },
};

// ==========================================
// NOTIFICATION MANAGER
// ==========================================
const NotificationManager = {
  show: function (message, type = "info", duration = 3000) {
    const notification = $(`
            <div class="toast-notification toast-${type}">
                <div class="toast-content">
                    <i class="fas fa-${this.getIcon(type)}"></i>
                    <span>${message}</span>
                </div>
                <button class="toast-close">
                    <i class="fas fa-times"></i>
                </button>
            </div>
        `);

    // Create container if not exists
    if (!$(".toast-container").length) {
      $("body").append('<div class="toast-container"></div>');
    }

    $(".toast-container").append(notification);

    // Show notification
    setTimeout(() => {
      notification.addClass("show");
    }, 100);

    // Auto hide
    setTimeout(() => {
      this.hide(notification);
    }, duration);

    // Close button
    notification.find(".toast-close").on("click", () => {
      this.hide(notification);
    });
  },

  hide: function (notification) {
    notification.removeClass("show");
    setTimeout(() => {
      notification.remove();
    }, 300);
  },

  getIcon: function (type) {
    const icons = {
      success: "check-circle",
      error: "exclamation-circle",
      warning: "exclamation-triangle",
      info: "info-circle",
    };
    return icons[type] || icons.info;
  },
};

// ==========================================
// UTILITY FUNCTIONS
// ==========================================
const Utils = {
  // Format currency
  formatCurrency: function (amount) {
    return new Intl.NumberFormat("vi-VN", {
      style: "currency",
      currency: "VND",
    }).format(amount);
  },

  // Debounce function
  debounce: function (func, wait) {
    let timeout;
    return function executedFunction(...args) {
      const later = () => {
        clearTimeout(timeout);
        func(...args);
      };
      clearTimeout(timeout);
      timeout = setTimeout(later, wait);
    };
  },

  // Check if element is in viewport
  isInViewport: function (element) {
    const rect = element.getBoundingClientRect();
    return (
      rect.top >= 0 &&
      rect.left >= 0 &&
      rect.bottom <=
        (window.innerHeight || document.documentElement.clientHeight) &&
      rect.right <= (window.innerWidth || document.documentElement.clientWidth)
    );
  },
};

// ==========================================
// GLOBAL EVENT HANDLERS
// ==========================================

// Handle window resize
$(window).on(
  "resize",
  Utils.debounce(function () {
    // Responsive adjustments
    if ($(window).width() > 991) {
      $(".navbar-collapse").removeClass("show");
      $(".navbar-toggler").removeClass("active");
    }
  }, 250)
);

// Handle page visibility change
document.addEventListener("visibilitychange", function () {
  if (document.hidden) {
    // Page is hidden
    console.log("Page hidden");
  } else {
    // Page is visible
    console.log("Page visible");
  }
});

// Back to top button
$(window).scroll(function () {
  if ($(this).scrollTop() > 300) {
    $("#backToTop").fadeIn().addClass("animate-pulse");
    // Remove pulse after 3 seconds
    setTimeout(() => {
      $("#backToTop").removeClass("animate-pulse");
    }, 3000);
  } else {
    $("#backToTop").fadeOut().removeClass("animate-pulse");
  }
});

// Create back to top button
$("body").append(`
    <button id="backToTop" class="back-to-top btn" title="Về đầu trang" aria-label="Lên đầu trang">
        <i class="fas fa-arrow-up"></i>
    </button>
`);

$("#backToTop").on("click", function () {
  $("html, body").animate({ scrollTop: 0 }, 600);
});

// Prevent form double submission
$("form").on("submit", function () {
  const submitBtn = $(this).find('button[type="submit"]');
  submitBtn.prop("disabled", true);
  setTimeout(() => {
    submitBtn.prop("disabled", false);
  }, 2000);
});
