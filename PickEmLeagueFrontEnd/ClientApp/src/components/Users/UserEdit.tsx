import React, { FormEvent, useContext, useEffect, useState } from "react";
import { Container, Form, Row } from "react-bootstrap";
import { useHistory, useParams } from "react-router-dom";
import { FormGroup, Label } from "reactstrap";
import { User } from "../../Apis";
import { UserContext } from "../../Data/Contexts/UserContext";
import { useEditUser } from "../../Data/User/useEditUser";
import { useGetUser } from "../../Data/User/useGetUser";

type UserEditParams = {
  userId: string;
};

export function UserEdit() {
  const history = useHistory();
  const { user } = useContext(UserContext);
  const [userToEdit, setUserToEdit] = useState<User>({});
  const id = parseInt(useParams<UserEditParams>().userId);

  useEffect(() => {
    async function GetUser() {
      setUserToEdit(await useGetUser(id));
    }
    GetUser();
  }, [id]);

  const handleSubmit = async (evt: FormEvent) => {
    evt.preventDefault();
    SaveUser(userToEdit);
    history.goBack();
  };

  const SaveUser = async (user: User): Promise<void> => {
    await useEditUser(user);
  };

  return (
    <div className="container">
      <div className="auth-wrapper">
        <div className="auth-inner">
          <form onSubmit={handleSubmit}>
            <div className="row form-group">
              <div className="col-3">
                <label>Name: </label>
              </div>
              <div className="col">
                <input
                  type="text"
                  value={userToEdit.name || ""}
                  onChange={(e) =>
                    setUserToEdit({ ...user, name: e.target.value })
                  }
                />
              </div>
            </div>
            <div className="row form-group">
              <div className="col-3">
                <label>Username: </label>
              </div>
              <div className="col">
                <input
                  type="text"
                  value={userToEdit.username || ""}
                  onChange={(e) =>
                    setUserToEdit({ ...user, username: e.target.value })
                  }
                />
              </div>
            </div>
            <div className="row form-group">
              <div className="col-3">
                <label>Email: </label>
              </div>
              <div className="col">
                <input
                  type="text"
                  value={userToEdit.email || ""}
                  onChange={(e) =>
                    setUserToEdit({ ...user, email: e.target.value })
                  }
                />
              </div>
            </div>
            <div className="row form-group">
              <div className="col-3">
                <label>Password: </label>
              </div>
              <div className="col">
                <input
                  type="text"
                  value={userToEdit.passwordHash || ""}
                  onChange={(e) =>
                    setUserToEdit({ ...user, passwordHash: e.target.value })
                  }
                />
              </div>
            </div>
            <div className="row form-group" hidden={!userToEdit.isAdmin}>
              <div className="col-3">
                <label>Admin: </label>
              </div>
              <div className="col">
                <input
                  type="checkbox"
                  checked={userToEdit.isAdmin || false}
                  onChange={(e) =>
                    setUserToEdit({ ...user, isAdmin: e.target.checked })
                  }
                />
              </div>
            </div>
            <input
              type="submit"
              value="Save"
              disabled={user?.id != userToEdit.id && !user?.isAdmin}
            />
          </form>
        </div>
      </div>
    </div>
  );
}
