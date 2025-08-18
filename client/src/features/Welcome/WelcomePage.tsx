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
  <Box sx={{
    position: 'absolute',
    top: 0,
    left: 0,
    width: 100,
    height: 100,
    backgroundColor: 'rgba(173,216,230,0.3)',
    transform: 'rotate(45deg)'
  }} />
  
  <Box sx={{
    position: 'absolute',
    bottom: 0,
    right: 0,
    width: 120,
    height: 120,
    backgroundColor: 'rgba(173,216,230,0.3)',
    transform: 'rotate(45deg)'
  }} />

  <Typography variant="h4" gutterBottom>
    A2C - The AI-powered CRM your team will love
  </Typography>

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
          ':hover': {
            borderColor: '#0c3c5d',
            backgroundColor: 'rgba(12,60,93,0.05)',
          },
        }}
      >
        {text}
      </Button>
    ))}
  </Box>
</Container>
  );
}
