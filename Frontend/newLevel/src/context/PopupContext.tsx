import{ createContext, ReactNode, useContext, useState } from 'react';

type MessageType = 'success' | 'info' | 'warning' | 'error';

type Message = {
  type: MessageType;
  text: string;
};

type AlertContextType = {
  message: Message | null;
  showAlert: (type: MessageType, text: string) => void;
  hideAlert: () => void;
};

const AlertContext = createContext<AlertContextType | undefined>(undefined);

type AlertProviderProps = {
  children: ReactNode;
};

export function AlertProvider({ children }: AlertProviderProps) {
  const [message, setMessage] = useState<Message | null>(null);

  const showAlert = (type: MessageType, text: string) => {
    setMessage({ type, text });

    // Limpar a mensagem após 3 segundos
    setTimeout(() => {
      hideAlert();
    }, 3000);
  };

  const hideAlert = () => {
    setMessage(null);
  };

  return (
    <AlertContext.Provider value={{ message, showAlert, hideAlert }}>
      {children}
    </AlertContext.Provider>
  );
}

export function useAlert() {
  const context = useContext(AlertContext);
  if (context === undefined) {
    throw new Error('useAlert must be used within an AlertProvider');
  }
  return context;
}
