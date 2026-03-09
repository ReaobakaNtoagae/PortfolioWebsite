# 👩‍💻 Reaobaka Ntoagae - Portfolio Website

A modern, feminine-coded portfolio website showcasing my journey as a Software Engineer. Built with love, creativity, and a touch of sparkle ✨

## 🌸 Features

- **Soft Feminine Design** - Pastel color palette with pink, peach, and lavender gradients
- **Gentle Animations** - Floating elements, heartbeat effects, and sparkle animations
- **Fully Responsive** - Looks beautiful on all devices from mobile to desktop
- **Interactive Elements** - Hover effects, typing animation, and smooth transitions
- **Contact Form** - Functional contact form with EmailJS integration
- **Project Showcase** - Grid layout displaying my best work
- **Skill Cards** - Categorized technical skills with soft hover effects
- **Glass Morphism** - Frosted glass effects on navigation bar
- **Custom Modal** - Beautiful popup for form submission feedback

## 🎨 Color Palette

| Color | Hex | Usage |
|-------|-----|-------|
| Pink | `#ff8a9c` | Primary accent, buttons |
| Peach | `#ffb8b8` | Secondary accent |
| Lavender | `#d4b8ff` | Tertiary accent |
| Soft Pink | `#ffd1dc` | Background accents |
| Soft Peach | `#ffe4e1` | Section backgrounds |
| Soft Lavender | `#e8d5ff` | Background accents |
| White | `#ffffff` | Card backgrounds |

## 🛠️ Technologies Used

### Frontend
- **HTML5** - Structure
- **CSS3** - Styling with custom properties
- **JavaScript** - Interactive functionality
- **Boxicons** - Icon library
- **Google Fonts** - Poppins font family
- **AOS Library** - Scroll animations

### Backend Services
- **EmailJS** - Contact form functionality

## 📁 Project Structure

```
portfolio-website/
│
├── 📄 index.html          # Main HTML file
├── 📄 style.css           # All styles and animations
├── 📄 README.md           # Project documentation
│
├── 🖼️ profilepic1.jpeg     # Profile picture
├── 🖼️ project1.png         # Management Hub project
├── 🖼️ movieapp.png         # Movie App project
├── 🖼️ bankingapp.png       # Banking Portal project
├── 🖼️ grindlyapp.png       # Grindly app project
└── 🖼️ municipal.png        # Municipal Services app
```

## ✨ Key Sections

### 1. **Header/Navigation**
- Sticky navigation with glass morphism effect
- Mobile-responsive hamburger menu
- Active link highlighting

### 2. **Home Section**
- Animated greeting with typing effect
- Professional title with gradient text
- Call-to-action buttons
- Floating profile picture with glow effect
- Social media links

### 3. **About Section**
- Personal introduction
- Experience badge with heart shape
- Statistics counter
- Read more button

### 4. **Skills Section**
- Categorized skill cards (Languages, Frameworks, Databases, Tools, Cloud)
- Color-coded icons (pink, peach, lavender)
- Hover effects with rotation and scaling

### 5. **Projects Section**
- Featured project cards
- Technology tags
- Hover overlay with links
- "Coming Soon" placeholder
- Project details and dates

### 6. **Contact Section**
- Contact information cards
- Availability status with pulsing dot
- Functional contact form with EmailJS
- Form validation and submission feedback

### 7. **Footer**
- Quick links
- Services offered
- Resource links
- Social media icons
- Back to top button

## 🚀 Installation & Setup

1. **Clone the repository**
```bash
git clone https://github.com/yourusername/portfolio-website.git
```

2. **Navigate to project directory**
```bash
cd portfolio-website
```

3. **Open with live server**
   - Use VS Code Live Server extension
   - Or simply open `index.html` in your browser

4. **Configure EmailJS (for contact form)**
   - Sign up at [EmailJS](https://www.emailjs.com/)
   - Create an email template
   - Update the following in `index.html`:
```javascript
emailjs.init('YOUR_USER_ID');  // Replace with your User ID
// Update service ID and template ID in the send function
emailjs.send('YOUR_SERVICE_ID', 'YOUR_TEMPLATE_ID', templateParams)
```

## 📱 Responsive Breakpoints

- **Desktop (1200px+)** - Full layout with all effects
- **Tablet (992px)** - Adjusted spacing and typography
- **Mobile (768px)** - Stacked layout, hamburger menu
- **Small Mobile (480px)** - Optimized for smallest screens

## 🎯 Customization Guide

### Change Colors
Edit the CSS variables in `:root`:
```css
:root {
    --pink: #ff8a9c;        /* Your preferred pink */
    --peach: #ffb8b8;        /* Your preferred peach */
    --lavender: #d4b8ff;      /* Your preferred lavender */
}
```

### Update Typing Effect
Modify the roles array in JavaScript:
```javascript
const roles = ['Software Engineer', 'Creative Coder', 'Problem Solver'];
```

### Add Projects
Copy the project card structure and update:
- Image source
- Title
- Description
- Tech tags
- Date

## ✨ Animations

- **Floating** - Profile picture and badges
- **Heartbeat** - Icons and buttons
- **Sparkle** - Decorative dots
- **Rotating** - Frame borders
- **Typing** - Dynamic role text
- **Fade In** - Scroll-triggered animations (AOS)
- **Hover Effects** - Scale, translate, and rotate

## 📧 Contact Form Setup

The contact form uses EmailJS. To make it work:

1. Create an account at [EmailJS](https://www.emailjs.com/)
2. Add your email service (Gmail, Outlook, etc.)
3. Create an email template
4. Get your User ID, Service ID, and Template ID
5. Update the JavaScript in the contact form section

## 🌟 Browser Support

- Chrome (latest)
- Firefox (latest)
- Safari (latest)
- Edge (latest)
- Opera (latest)

## 📄 License

© 2024 Reaobaka Ntoagae. All rights reserved.

## 🤝 Connect With Me

- GitHub: [Reaobaka Ntoagae](ReaobakaNtoagae )
- LinkedIn: [Reaobaka Ntoagae](linkedin.com/in/reaobaka-ntoagae-697594292)

## 🙏 Acknowledgments

- Icons by [Boxicons](https://boxicons.com/)
- Fonts by [Google Fonts](https://fonts.google.com/)
- Animations by [AOS Library](https://michalsnik.github.io/aos/)
- Email service by [EmailJS](https://www.emailjs.com/)

---

<p align="center">
  Made with <i class='bx bx-heart'></i> and code by Reaobaka Ntoagae
  <br>
  <sub>✨ Always learning, always growing ✨</sub>
</p>
