import React, { createContext, useContext, useState, useMemo } from "react";

type AuthContextType = {
  token: string | null;
  login: (token: string) => void;
  logout: () => void;
  isAuthenticated: boolean;
};

const AuthContext = createContext<AuthContextType | undefined>(undefined);

export const AuthProvider: React.FC<React.PropsWithChildren> = ({ children }) => {
  const [token, setToken] = useState<string | null>(
    () => localStorage.getItem("authToken")
  );

  const login = (t: string) => {
    localStorage.setItem("authToken", t);
    setToken(t);
  };

  const logout = () => {
    localStorage.removeItem("authToken");
    setToken(null);
  };

  const value = useMemo(
    () => ({ token, login, logout, isAuthenticated: !!token }),
    [token]
  );

  return <AuthContext.Provider value={value}>{children}</AuthContext.Provider>;
};

export const useAuth = () => {
  const ctx = useContext(AuthContext);
  if (!ctx) throw new Error("useAuth must be used inside AuthProvider");
  return ctx;
};
