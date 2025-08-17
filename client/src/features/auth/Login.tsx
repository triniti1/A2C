import React from 'react';
import { useForm } from 'react-hook-form';
import { TextField, Button, Box, Typography, Container } from '@mui/material';
import * as yup from 'yup';
import { yupResolver } from '@hookform/resolvers/yup';
import axios from 'axios';
import { toast, ToastContainer } from 'react-toastify';
import 'react-toastify/dist/ReactToastify.css';

// Define the shape of the form data
type LoginFormInputs = {
  email: string;
  password: string;
};

// Define validation schema using Yup
const schema = yup.object().shape({
  email: yup.string().email('Invalid email address').required('Email is required'),
  password: yup.string().min(6, 'Password must be at least 6 characters').required('Password is required'),
});

export default function Login() {
  // Initialize the form with validation
  const {
    register,
    handleSubmit,
    formState: { errors },
  } = useForm<LoginFormInputs>({
    resolver: yupResolver(schema),
  });


const onSubmit = async (data: LoginFormInputs) => {
  try {

    const url = `${process.env.REACT_APP_API_BASE_URL}/api/auth/login`;
   
    //console.log('onSubmit url is:', url);
    const response = await axios.post(url, data);
    const token = response.data.token;

    // Save token to localStorage
    localStorage.setItem('authToken', token);

    toast.success('Login successful!');
    console.log('Token saved:', token);

    // TODO: Redirect to another page
  } catch (error: any) {
    toast.error('Login failed. Please check your credentials.');
    console.error('Login error:', error);
  }
};


  return (
    <Container maxWidth="xs">
      <Box sx={{ mt: 8 }}>
        <Typography variant="h5" align="center" gutterBottom>
          Login to A2C CRM
        </Typography>
        <form onSubmit={handleSubmit(onSubmit)}>
          <TextField
            label="Email"
            fullWidth
            margin="normal"
            {...register('email')}
            error={!!errors.email}
            helperText={errors.email?.message}
          />
          <TextField
            label="Password"
            type="password"
            fullWidth
            margin="normal"
            {...register('password')}
            error={!!errors.password}
            helperText={errors.password?.message}
          />
          <Button
            type="submit"
            variant="contained"
            color="primary"
            fullWidth
            sx={{ mt: 2 }}
          >
            Login
          </Button>
        </form>
        <ToastContainer />
      </Box>
    </Container>
  );
}
