document.addEventListener('DOMContentLoaded', function() {
  const loginForm = document.querySelector('form');
  const emailInput = document.getElementById('email');
  const passwordInput = document.getElementById('password');

  loginForm.addEventListener('submit', async function(e) {
    e.preventDefault(); // Prevent the form from submitting the traditional way

    const email = emailInput.value.trim();
    const password = passwordInput.value.trim();

    if (!email || !password) {
      alert("Please fill in both email and password.");
      return;
    }

    const loginData = {
      Email: email,
      Password: password
    };

    try {
      const response = await fetch('https://localhost:5001/api/Auth/login', {
        method: 'POST',
        headers: {
          'Content-Type': 'application/json'
        },
        body: JSON.stringify(loginData)
      });

      if (response.ok) {
        const result = await response.json();
        alert(result.message); // Show the success message
        window.location.href = "/Home/LoginSuccess"; // Redirect to the LoginSuccess page
      } else {
        const error = await response.json();
        alert(error.message); // Show error message
      }
    } catch (error) {
      console.error('Error:', error);
      alert('An error occurred while logging in. Please try again later.');
    }
  });
});