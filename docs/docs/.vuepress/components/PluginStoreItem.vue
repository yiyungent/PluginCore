<template>
  <div class="resource-card-container">
    <div class="resource-card-header">
      <div class="resource-card-header-title">
        {{ plugin.displayName }}
        <FontAwesomeIcon
          class="resource-card-header-icon-check"
          :icon="['fas', 'circle-check']"
        />
      </div>
      <div class="resource-card-header-expand">
        <FontAwesomeIcon
          class="resource-card-header-icon-expand"
          :icon="['fas', 'expand']"
        />
      </div>
    </div>
    <div class="resource-card-desc">{{ plugin.description }}</div>
    <div class="resource-card-footer">
      <div class="resource-card-footer-tags">
        <span
          v-for="tag in plugin.tags"
          :key="tag"
          class="resource-card-footer-tag"
          >{{ tag }}</span
        >
      </div>
      <div class="resource-card-footer-divider"></div>
      <div class="resource-card-footer-info">
        <div class="resource-card-footer-group">
          <a
            v-show="!!plugin.homepageUrl"
            :href="plugin.homepageUrl"
            target="_blank"
            rel="noopener noreferrer"
          >
            <FontAwesomeIcon
              v-show="homepageUrlIsGitHub()"
              class="resource-card-footer-icon"
              :icon="['fab', 'github']"
            />
            <FontAwesomeIcon
              v-show="!homepageUrlIsGitHub()"
              class="resource-card-footer-icon"
              :icon="['fas', 'link']"
            />
          </a>
          <a
            v-show="isNuget"
            :href="nugetUrl"
            target="_blank"
            alt="NuGet"
            rel="noopener noreferrer"
          >
            <FontAwesomeIcon
              class="resource-card-footer-icon"
              :icon="['fas', 'cube']"
            />
          </a>
        </div>
        <div class="resource-card-footer-group">
          <div class="avatar" v-show="!!githubOwner">
            <div class="resource-card-footer-avatar">
              <a
                :href="`https://github.com/${githubOwner}`"
                target="_blank"
                rel="noopener noreferrer"
                ><img :src="`https://github.com/${githubOwner}.png?size=80`"
              /></a>
            </div>
          </div>
          <span class="resource-card-footer-author">{{ plugin.author }}</span>
        </div>
      </div>
    </div>
  </div>
</template>
<script>
import { FontAwesomeIcon } from "@fortawesome/vue-fontawesome";
import { library } from "@fortawesome/fontawesome-svg-core";
import { fas } from "@fortawesome/free-solid-svg-icons";
import { fab } from "@fortawesome/free-brands-svg-icons";
library.add(fas, fab);


export default {
  components: {
    FontAwesomeIcon,
  },

  props: {
    plugin: {
      type: Object,
      required: true,
      default: function() {
        return {};
      },
    },
  },

  data() {
    return {};
  },

  computed: {
    isNuget() {
      return !!this.plugin.nugetPackageId;
    },
    nugetUrl() {
      return `https://www.nuget.org/packages/${this.plugin.nugetPackageId}`;
    },
    githubOwner() {
        return this.plugin.github && this.plugin.github.split("/").length >= 2 && this.plugin.github.split("/")[0];
    },
  },

  created() {},

  methods: {
    homepageUrlIsGitHub() {
      return /^https:\/\/github.com\/[^/]+\/[^/]+/.test(
        this.plugin.homepageUrl
      );
    },
  },
};
</script>
<style scoped>
.resource-card-container {
  box-sizing: border-box;
  border: 0px solid rgb(229, 231, 235);
  background-color: hsl(0 0% 95%/1);
  border-color: hsl(0 0% 95%/1);
  border-radius: 0.5rem;
  border-width: 2px;
  display: flex;
  flex-direction: column;
  min-height: 12rem;
  padding: 1rem;
  row-gap: 0.5rem;
  transition-duration: 0.5s;
  transition-property: color, background-color, border-color,
    text-decoration-color, fill, stroke;
  transition-timing-function: cubic-bezier(0.4, 0, 0.2, 1);
  width: 100%;
}

.resource-card-container:focus {
  outline: 0px;
}

.resource-card-container:hover {
  border-color: rgb(78, 201, 176);
}

.resource-card-header {
  box-sizing: border-box;
  border: 0px solid rgb(229, 231, 235);
  align-items: center;
  display: flex;
  font-size: 1.125rem;
  font-weight: 500;
  line-height: 1.75rem;
  width: 100%;
}

.resource-card-header:focus {
  outline: 0px;
}

.resource-card-header-title {
  box-sizing: border-box;
  border: 0px solid rgb(229, 231, 235);
  flex-grow: 1;
  overflow: hidden;
  text-align: left;
  text-overflow: ellipsis;
  white-space: nowrap;
}

.resource-card-header-title:focus {
  outline: 0px;
}

.resource-card-header-icon-check {
  border: 0px solid rgb(229, 231, 235);
  vertical-align: -0.125em;
  display: inline-block;
  fill: currentcolor;
  color: hsl(158 64% 52%/1);
  height: 1.25rem;
  margin-left: 0.5rem;
  width: 1.25rem;
  box-sizing: content-box;
  overflow: visible;
}

.resource-card-header-icon-check:focus {
  outline: 0px;
}

.pluginstore-5 {
  box-sizing: border-box;
  border: 0px solid rgb(229, 231, 235);
}

.pluginstore-5:focus {
  outline: 0px;
}

.resource-card-header-expand {
  box-sizing: border-box;
  border: 0px solid rgb(229, 231, 235);
  fill: currentcolor;
  cursor: pointer;
  flex: 0 0 auto;
}

.resource-card-header-expand:focus {
  outline: 0px;
}

.resource-card-header-icon-expand {
  border: 0px solid rgb(229, 231, 235);
  vertical-align: -0.125em;
  height: 1em;
  display: inline-block;
  box-sizing: content-box;
  overflow: visible;
}

.resource-card-header-icon-expand:focus {
  outline: 0px;
}

.pluginstore-8 {
  box-sizing: border-box;
  border: 0px solid rgb(229, 231, 235);
}

.pluginstore-8:focus {
  outline: 0px;
}

.resource-card-desc {
  box-sizing: border-box;
  border: 0px solid rgb(229, 231, 235);
  flex: 1 1 0%;
  font-size: 0.875rem;
  line-height: 1.25rem;
  overflow-wrap: break-word;
  text-overflow: ellipsis;
  width: 100%;
}

.resource-card-desc:focus {
  outline: 0px;
}

.resource-card-footer {
  box-sizing: border-box;
  border: 0px solid rgb(229, 231, 235);
  cursor: default;
  display: flex;
  flex-direction: column;
  width: 100%;
}

.resource-card-footer:focus {
  outline: 0px;
}

.resource-card-footer-tags {
  box-sizing: border-box;
  border: 0px solid rgb(14, 94, 255);
  display: flex;
  flex-wrap: wrap;
  gap: 0.25rem;
}

.resource-card-footer-tags:focus {
  outline: 0px;
}

.resource-card-footer-tag {
  box-sizing: border-box;
  border: 0px solid rgb(14, 94, 255);
  cursor: pointer;
  font-family: ui-monospace, SFMono-Regular, Menlo, Monaco, Consolas,
    "Liberation Mono", "Courier New", monospace;
  align-items: center;
  border-radius: 0.25rem;
  display: inline-flex;
  font-size: 0.75rem;
  height: 1.25rem;
  justify-content: center;
  line-height: 1rem;
  padding-left: 0.5rem;
  padding-right: 0.5rem;
  width: fit-content;
  background-color: rgb(78, 201, 176);
  color: rgb(255, 255, 255);
}

.resource-card-footer-tag:focus {
  outline: 0px;
}

.resource-card-footer-divider {
  box-sizing: border-box;
  border: 0px solid rgb(229, 231, 235);
  margin: 0px;
  align-items: center;
  align-self: stretch;
  display: flex;
  flex-direction: row;
  height: 1rem;
  margin-bottom: 1rem;
  margin-top: 1rem;
  white-space: nowrap;
}

.resource-card-footer-divider:focus {
  outline: 0px;
}

.resource-card-footer-divider:before {
  content: "";
  flex-grow: 1;
  height: 0.125rem;
  width: 100%;
  background-color: #dcdddf;
}

.resource-card-footer-divider:after {
  content: "";
  flex-grow: 1;
  height: 0.125rem;
  width: 100%;
  background-color: #dcdddf;
}

.resource-card-footer-info {
  box-sizing: border-box;
  border: 0px solid rgb(229, 231, 235);
  align-items: center;
  display: flex;
  justify-content: space-between;
  width: 100%;
}

.resource-card-footer-info:focus {
  outline: 0px;
}

.resource-card-footer-group {
  box-sizing: border-box;
  border: 0px solid rgb(229, 231, 235);
  align-items: center;
  column-gap: 0.25rem;
  display: flex;
  justify-content: center;
  line-height: 1;
}

.resource-card-footer-group:focus {
  outline: 0px;
}

a {
  color: inherit;
  text-decoration: inherit;
}

.resource-card-footer-icon {
  border: 0px solid rgb(229, 231, 235);
  vertical-align: -0.125em;
  display: inline-block;
  fill: currentcolor;
  height: 1.25rem;
  opacity: 0.8;
  width: 1.25rem;
  box-sizing: content-box;
  overflow: visible;
}

.resource-card-footer-icon:focus {
  outline: 0px;
}

.resource-card-footer-icon:hover {
  opacity: 1;
}

.pluginstore-18 {
  box-sizing: border-box;
  border: 0px solid rgb(229, 231, 235);
}

.pluginstore-18:focus {
  outline: 0px;
}

.pluginstore-19 {
  box-sizing: border-box;
  border: 0px solid rgb(229, 231, 235);
  color: inherit;
  text-decoration: inherit;
}

.pluginstore-19:focus {
  outline: 0px;
}

.pluginstore-20 {
  border: 0px solid rgb(229, 231, 235);
  vertical-align: -0.125em;
  display: inline-block;
  fill: currentcolor;
  height: 1.25rem;
  opacity: 0.8;
  width: 1.25rem;
  box-sizing: content-box;
  overflow: visible;
}

.pluginstore-20:focus {
  outline: 0px;
}

.pluginstore-20:hover {
  opacity: 1;
}

.pluginstore-21 {
  box-sizing: border-box;
  border: 0px solid rgb(229, 231, 235);
}

.pluginstore-21:focus {
  outline: 0px;
}

.avatar {
  box-sizing: border-box;
  border: 0px solid rgb(229, 231, 235);
  position: relative;
  display: inline-flex;
}

.avatar:focus {
  outline: 0px;
}

.resource-card-footer-avatar {
  box-sizing: border-box;
  border: 0px solid rgb(229, 231, 235);
  border-radius: 9999px;
  height: 1.25rem;
  transition-duration: 0.15s;
  transition-property: box-shadow;
  transition-timing-function: cubic-bezier(0.4, 0, 0.2, 1);
  width: 1.25rem;
  aspect-ratio: 1 / 1;
  display: block;
  overflow: hidden;
}

.resource-card-footer-avatar:focus {
  outline: 0px;
}

.resource-card-footer-avatar:hover {
  box-shadow: 0 0 #0000, 0 0 #0000, 0 0 #0000;
}

.pluginstore-25 {
  box-sizing: border-box;
  border: 0px solid rgb(229, 231, 235);
  color: inherit;
  text-decoration: inherit;
}

.pluginstore-25:focus {
  outline: 0px;
}

.avatar img {
  box-sizing: border-box;
  border: 0px solid rgb(229, 231, 235);
  display: block;
  max-width: 100%;
  height: 100%;
  object-fit: cover;
  width: 100%;
}

.resource-card-footer-author {
  box-sizing: border-box;
  border: 0px solid rgb(229, 231, 235);
  cursor: pointer;
  font-size: 0.875rem;
  line-height: 1.25rem;
}

.resource-card-footer-author:focus {
  outline: 0px;
}
</style>
