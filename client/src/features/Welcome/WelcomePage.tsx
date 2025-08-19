import { Box, Button, Container, Typography } from "@mui/material";
import { useAuth } from "../../store/AuthContext";
import { useNavigate } from "react-router-dom";

export default function WelcomePage() {
  const { logout } = useAuth();
  const navigate = useNavigate();

  return (
    <Container
  sx={{
    position: 'relative',
    minHeight: '100vh',
    display: 'flex',
    flexDirection: 'column',
    alignItems: 'center',
    justifyContent: 'center',
    backgroundColor: '#fff',
    overflow: 'hidden',
    textAlign: 'center',
  }}
>
  {/* צורות גיאומטריות */}
  {/* צורה עליונה ימנית */}
<Box sx={{
  position: 'absolute',
  top: -30,
  right: 60,
  width: 120,
  height: 120,
  backgroundColor: 'rgba(150,200,240,0.25)',
  transform: 'rotate(30deg)',
}} />

<Box sx={{
  position: 'absolute',
  top: 30,
  right: 60,
  width: 120,
  height: 120,
  backgroundColor: 'rgba(153, 183, 207, 0.25)',
  transform: 'rotate(30deg)',
}} />

{/* צורה תחתונה שמאלית */}
<Box sx={{
  position: 'absolute',
  bottom: -40,
  left: 60,
  width: 140,
  height: 140,
  backgroundColor: 'rgba(80,160,200,0.2)',
  transform: 'rotate(60deg)',
}} />
<Box sx={{
  position: 'absolute',
  bottom: 60,
  left: 180,
  width: 140,
  height: 140,
  backgroundColor: 'rgba(2, 19, 27, 0.2)',
  transform: 'rotate(60deg)',
}} />

{/* צורה תחתונה שמאלית */}
<Box sx={{
  position: 'absolute',
  bottom: 740,
  left: -60,
  width: 140,
  height: 140,
  backgroundColor: 'rgba(80,160,200,0.2)',
  transform: 'rotate(60deg)',
}} />
<Box sx={{
  position: 'absolute',
  bottom: 760,
  left: -60,
  width: 140,
  height: 140,
  backgroundColor: 'rgba(2, 19, 27, 0.2)',
  transform: 'rotate(90deg)',
}} />



{/* צורה תחתונה ימנית */}
<Box sx={{
  position: 'absolute',
  bottom: -20,
  right: 20,
  width: 160,
  height: 160,
  backgroundColor: 'rgba(120,190,230,0.15)',
  transform: 'rotate(15deg)',
  borderRadius: 4,
}} />

<Box sx={{
  position: 'absolute',
  bottom: 40,
  right: 20,
  width: 160,
  height: 160,
  backgroundColor: 'rgba(207, 154, 38, 0.15)',
  transform: 'rotate(15deg)',
  borderRadius: 4,
}} />

  <Box sx={{ mt: -8 }}>
  <Typography
    variant="h4"
    gutterBottom
    fontWeight={200}
    sx={{ color: '#0c3c5d' }}
  >
    A2C - The AI-powered CRM your team will love
  </Typography>
</Box>

  {/* כפתורים */}
  <Box sx={{
    display: 'flex',
    flexWrap: 'wrap',
    justifyContent: 'center',
    mt: 4,
    gap: 3,
  }}>
    {['Users', 'Customers', 'Products', 'Orders'].map((text) => (
      <Button
        key={text}
        variant="outlined"
        sx={{
          borderWidth: 1,
          borderColor: 'rgba(0,0,0,0.3)',
          borderRadius: 2,
          minWidth: 150,
          minHeight: 50,
          color: '#0c3c5d',
          fontWeight: 500,
          transition: 'transform 0.2s ease, background-color 0.2s ease, border-color 0.2s ease',
          ':hover': {
            borderColor: '#0c3c5d',
            backgroundColor: 'rgba(12,60,93,0.05)',
            transform: 'scale(1.05)',   // הגדלה עדינה
          },
          ':active': {
            transform: 'scale(0.98)',   // לחיצה - הקטנה קלה
          },
        }}
          onClick={() => {
              if (text === "Users") {
                navigate("/users");
              } else {
                console.log(`${text} button clicked`);
              }
            }}
      >
        {text}
      </Button>
    ))}
  </Box>
</Container>
  );
}
