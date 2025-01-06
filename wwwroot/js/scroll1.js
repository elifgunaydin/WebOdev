document.addEventListener('scroll', function () {
    const wrappers = document.querySelectorAll('.hizmetler .wrapper');
    wrappers.forEach(wrapper => {
        const rect = wrapper.getBoundingClientRect();
        if (rect.top < window.innerHeight && rect.bottom > 0) {
            wrapper.classList.add('active');
        } else {
            wrapper.classList.remove('active');
        }
    });
});