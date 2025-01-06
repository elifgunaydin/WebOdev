const homeBody = document.querySelectorAll('.homeBody');

const options = {
    threshold: 0.3
};

const callBack = (entries) => {
    entries.forEach((entry) => {
        if (entry.isIntersecting) {
            entry.target.classList.add('active');
        } else {
            entry.target.classList.remove('active');
        }
    });
};

const observer = new IntersectionObserver(callBack, options);

homeBody.forEach((target) => {
    observer.observe(target);
});


