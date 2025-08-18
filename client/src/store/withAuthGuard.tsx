import React from "react";
import { useAuth } from "./AuthContext";
import { Navigate } from "react-router-dom";

function withAuthGuard<P extends object>(WrappedComponent: React.ComponentType<P>) {
  return function GuardedComponent(props: P) {
    const { isAuthenticated } = useAuth();

    if (!isAuthenticated) {
      return <Navigate to="/login" replace />;
    }

    return <WrappedComponent {...props} />;
  };
}

export default withAuthGuard;
