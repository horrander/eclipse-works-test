CREATE DATABASE eclipseworks

CREATE EXTENSION IF NOT EXISTS "uuid-ossp";

CREATE TABLE public.users (
	id uuid DEFAULT uuid_generate_v4() NOT NULL,
	email varchar(100) NOT NULL,
	created_at timestamp DEFAULT now() NOT NULL,
	modified_at timestamp NULL,
	removed_at timestamp NULL,
	CONSTRAINT users_pkey PRIMARY KEY (id)
);

CREATE TABLE public.projects (
	id uuid DEFAULT uuid_generate_v4() NOT NULL,
	title varchar(100) NOT NULL,
	description varchar(500) NULL,
	user_id uuid NOT NULL,
	created_at timestamp DEFAULT now() NOT NULL,
	modified_at timestamp NULL,
	removed_at timestamp NULL,
	CONSTRAINT projects_pkey PRIMARY KEY (id)
);

CREATE TABLE public.tasks (
	id uuid DEFAULT uuid_generate_v4() NOT NULL,
	title varchar(100) NOT NULL,
	description varchar(500) NULL,
	due_date timestamp NOT NULL,
	status int4 NOT NULL,
	priority int4 NOT NULL,
	project_id uuid NOT NULL,
	created_at timestamp DEFAULT now() NOT NULL,
	modified_at timestamp NULL,
	removed_at timestamp NULL,
	CONSTRAINT tasks_pkey PRIMARY KEY (id)
);
