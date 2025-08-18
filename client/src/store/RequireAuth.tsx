import { Navigate, useLocation } from "react-router-dom";
import { useAuth } from "./AuthContext";
import { ReactNode } from "react";

type Props = {
  children: ReactNode;
};

export default function RequireAuth({ children }: Props) {
  const { isAuthenticated } = useAuth();
  const location = useLocation();

  if (!isAuthenticated) {
    // אם לא מחובר – נשלח ל-login
    return <Navigate to="/login" state={{ from: location }} replace />;
  }

  return children;
}