import React, { FormEvent, useContext, useEffect, useState } from "react";
import { useHistory, useParams } from "react-router-dom";
import { User } from "../../Apis";
import { UserContext } from "../../Data/Contexts/UserContext";
import { SetUserImage } from "../../Data/Images/useSetUserImage";
import { useEditUser } from "../../Data/User/useEditUser";
import { useGetUser } from "../../Data/User/useGetUser";
import { ProfilePicture } from "../Images/ProfilePicture";

type UserEditParams = {
  userId: string;
};

async function GetUserExt(id: number) {
  return await useGetUser(id);
}

export function UserEdit() {
  const history = useHistory();
  const { user } = useContext(UserContext);
  const [userToEdit, setUserToEdit] = useState<User>({});
  const [file, setFile] = useState<File | null>();
  const id = parseInt(useParams<UserEditParams>().userId);

  useEffect(() => {
    async function GetUser() {
      setUserToEdit(await GetUserExt(id));
    }
    GetUser();
  }, [id]);

  const handleSubmit = async (evt: FormEvent) => {
    evt.preventDefault();
    SaveUser(userToEdit);
    history.goBack();
  };

  const handleFileChange = async (evt: React.ChangeEvent<HTMLInputElement>) => {
    if (evt.target.files) {
      setFile(evt.target.files[0]);
      //TODO: should this be userToEdit?
      SetUserImage(user?.id!, evt.target.files[0]);
      setUserToEdit(await GetUserExt(id));
    } else {
      setFile(null);
    }
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
                    setUserToEdit({ ...userToEdit, name: e.target.value })
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
                    setUserToEdit({ ...userToEdit, username: e.target.value })
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
                    setUserToEdit({ ...userToEdit, email: e.target.value })
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
                    setUserToEdit({
                      ...userToEdit,
                      passwordHash: e.target.value,
                    })
                  }
                />
              </div>
            </div>
            <div className="row form-group" hidden={!user?.isAdmin}>
              <div className="col-3">
                <label>Admin: </label>
              </div>
              <div className="col">
                <input
                  type="checkbox"
                  checked={userToEdit.isAdmin || false}
                  onChange={(e) =>
                    setUserToEdit({ ...userToEdit, isAdmin: e.target.checked })
                  }
                />
              </div>
            </div>
            <div className="row form-group" hidden={!user?.isAdmin}>
              <div className="col-3">
                <label>Missed Weeks: </label>
              </div>
              <div className="col">
                <input
                  type="text"
                  value={userToEdit.missedWeeks?.join(",")}
                  //TODO: when you type the comma this causes an NaN to show up.
                  onChange={(e) =>
                    setUserToEdit({ ...userToEdit, 
                      missedWeeks: e.target.value.split(",")
                                      .map(function(item) 
                                        {
                                          return parseInt(item, 10);
                                        }
                                      )
                                  })
                  }
                />
              </div>
            </div>
            <div className="row form-group">
              <input type="file" onChange={handleFileChange} />
              <label>{"File name: " + file?.name}</label>
            </div>
            <ProfilePicture userId={userToEdit.id} />
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
