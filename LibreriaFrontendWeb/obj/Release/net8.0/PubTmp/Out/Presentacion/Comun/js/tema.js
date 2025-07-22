// Theme management
if (typeof themeManager === 'undefined') {
    var themeManager = {
        init() {
            // Check for saved theme preference or default to light
            const savedTheme = localStorage.getItem('theme') || 'light';
            this.setTheme(savedTheme);

            // Set up theme toggle button if it exists
            this.setupThemeToggle();
        },

        setTheme(theme) {
            if (theme === 'dark') {
                document.documentElement.classList.add('dark-theme');
                document.body.classList.add('dark-theme');
            } else {
                document.documentElement.classList.remove('dark-theme');
                document.body.classList.remove('dark-theme');
            }

            localStorage.setItem('theme', theme);

            // Update theme toggle button icon if it exists
            const themeToggleBtn = document.getElementById('themeToggle');
            if (themeToggleBtn) {
                const icon = themeToggleBtn.querySelector('i');
                if (icon) {
                    icon.className = theme === 'dark' ? 'bi bi-sun-fill' : 'bi bi-moon-fill';
                }
            }
        },

        toggleTheme() {
            const currentTheme = localStorage.getItem('theme') || 'light';
            const newTheme = currentTheme === 'light' ? 'dark' : 'light';
            this.setTheme(newTheme);
        },

        setupThemeToggle() {
            const themeToggleBtn = document.getElementById('themeToggle');
            if (themeToggleBtn) {
                themeToggleBtn.addEventListener('click', () => this.toggleTheme());
            }
        }
    };
}

// Initialize theme when DOM is loaded
if (document.readyState === 'loading') {
    document.addEventListener('DOMContentLoaded', () => {
        themeManager.init();
    });
} else {
    // DOMContentLoaded has already fired
    themeManager.init();
}