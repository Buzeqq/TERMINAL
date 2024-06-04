import { createFeatureSelector } from "@ngrx/store";
import { Identity } from "../../identity/identity.model";
import { identityFeatureKey } from "./identity.reducer";

export const selectIdentity =
  createFeatureSelector<Readonly<Identity>>(identityFeatureKey);
