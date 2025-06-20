/*
 * ==========================================
 * HOME VIDEO HERO - JavaScript
 * Website: Đồ Tráng Miệng
 * ==========================================
 */

$(document).ready(function () {
  console.log("Home video hero JS loaded!");

  // ==========================================
  // VIDEO CONTROLS
  // ==========================================
  const video = document.getElementById("heroVideo");

  if (video) {
    // Ensure video plays on load
    video.play().catch(function (error) {
      console.log("Video autoplay failed:", error);
    });

    // Pause video when not in viewport (performance optimization)
    const observer = new IntersectionObserver((entries) => {
      entries.forEach((entry) => {
        if (entry.isIntersecting) {
          video.play();
        } else {
          video.pause();
        }
      });
    });

    observer.observe(video);
  }

  // ==========================================
  // SMOOTH SCROLL FUNCTIONALITY
  // ==========================================
  $(".scroll-indicator").on("click", function (e) {
    e.preventDefault();

    // Find next section after hero
    const nextSection = $(".video-hero").next();

    if (nextSection.length) {
      $("html, body").animate(
        {
          scrollTop: nextSection.offset().top,
        },
        800,
        "easeInOutCubic"
      );
    }
  });

  // ==========================================
  // PARALLAX EFFECT FOR VIDEO
  // ==========================================
  $(window).on("scroll", function () {
    const scrolled = $(window).scrollTop();
    const videoHero = $(".video-hero");
    const videoContainer = $(".video-container");

    if (videoHero.length && scrolled < videoHero.height()) {
      // Parallax effect for video
      const parallaxSpeed = 0.5;
      videoContainer.css(
        "transform",
        `translateY(${scrolled * parallaxSpeed}px)`
      );

      // Fade out effect
      const opacity = 1 - scrolled / videoHero.height();
      $(".video-overlay").css("opacity", Math.max(0.3, opacity));
    }
  });

  // ==========================================
  // HERO BUTTONS ANIMATIONS
  // ==========================================
  $(".btn-hero-primary, .btn-hero-secondary")
    .on("mouseenter", function () {
      $(this).addClass("animate__animated animate__pulse");
    })
    .on("mouseleave", function () {
      $(this).removeClass("animate__animated animate__pulse");
    });

  // ==========================================
  // VIDEO FALLBACK HANDLING
  // ==========================================
  if (video) {
    video.addEventListener("error", function () {
      console.log("Video failed to load, showing fallback image");
      $(".video-container").addClass("video-error");
    });
  }

  console.log("Home video hero setup complete!");
});
