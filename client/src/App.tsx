import { BrowserRouter, Routes, Route, Navigate } from "react-router-dom";
import Login from "./features/auth/Login";
import WelcomePage from "./features/Welcome/WelcomePage"
import withAuthGuard from "./store/withAuthGuard";

import { AuthProvider } from "./store/AuthContext";

const ProtectedWelcome = withAuthGuard(WelcomePage);

function App() {
  return (
    <AuthProvider>
      <BrowserRouter>
        <Routes>
            <Route path="/login" element={<Login />} />
            <Route path="/welcome" element={<ProtectedWelcome />} />
            <Route path="*" element={<Navigate to="/login" replace />} />
        </Routes>
      </BrowserRouter>
    </AuthProvider>
  );
}

export default App;
