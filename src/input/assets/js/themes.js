let theme = localStorage.getItem('theme');

const darkModeSwitch = document.getElementById('dark-mode-switch');
const checkbox = darkModeSwitch.getElementsByTagName('input')[0];

const enableDarkMode = () => {
    localStorage.setItem('theme', 'dark');
    checkbox.checked = true;
    document.body.dataset.theme = 'dark';
}

const disableDarkMode = () => {
    localStorage.setItem('theme', 'light');
    checkbox.checked = false;
    document.body.dataset.theme = 'light';
}

if (theme === 'dark') {
    enableDarkMode();
} else {
    disableDarkMode();
}

checkbox.addEventListener('click', () => {
    theme = localStorage.getItem('theme');
    if (theme !== 'dark') {
        enableDarkMode();
    } else {
        disableDarkMode();
    }
});